from audio import Audio
import argparse


def main(source, destination):
    trimmed = Audio.trim_silence_from_file(source)
    Audio.save_audio(destination, trimmed)


if __name__ == "__main__":
    text = "This program will trim leading and trailing silence from a wav file."

    parser = argparse.ArgumentParser(description=text)

    parser.add_argument("--source", "-s", help="Source wav file to trim", required=True)
    parser.add_argument("--destination", "-d", help="Destination of trimmed wav file", required=True)

    args = parser.parse_args()

    source = args.source
    destination = args.destination

    print(f"Source: {source}")
    print(f"Destination: {destination}")

    main(source, destination)
