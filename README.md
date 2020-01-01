# Satellite Audio Recorder

## Overview
This is a tool designed to assist with creating Audio Drama when actors are primarily located remotely.  It manages the collection of one or more recordings of each line of dialog  in the script.  

### Workflow
The following workflow is imagined for the collection of the audio.

1. A script that is ready for production is imported into the system.  This S.A.R. can import Fountain formatted scripts.  See https://fountain.io for programs that are compatible with this format.
2. Actors are assigned to specific characters in the script.
3. The Actors are guided through the lines of dialogue that they are required to record. 
4. The director reviews the audio and provides feedback.  They can also request that additional takes be made of specific lines of dialogue.
5. Actors complete any needed retakes.
6. The Audio engineer extracts all audio into a standardized folder structure for processing in an audio editing tool like Audacity or Audition.

### What it doesn't do
1.  Though you can make minor changes to scripts in this tool, it is not intended to be a script writing platform.  There are many other tools that are streamlined for script writing use one of them (Final Draft, Celtx, etc.).
2.  It is not intended to be used to edit, mix or otherwise produce the final audio output of the audio drama.

## How to set it up

S.A.R. is designed to be run in a Docker Container that is hosted on a Linux container.  

