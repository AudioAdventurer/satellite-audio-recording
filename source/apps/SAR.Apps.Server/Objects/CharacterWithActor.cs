using SAR.Modules.Script.Objects;

namespace SAR.Apps.Server.Objects
{
    public class CharacterWithActor : Character
    {
        public CharacterWithActor()
        {
        }

        public CharacterWithActor(Character c)
        {
            this.Id = c.Id;
            this.ProjectId = c.ProjectId;
            this.Name = c.Name;
            this.ActorPersonId = c.ActorPersonId;
        }

        public Person Actor { get; set; }
    }
}
