using SAR.Modules.Script.Objects;

namespace SAR.Apps.Server.Objects
{
    public class CharacterWithPerformer : Character
    {
        public CharacterWithPerformer()
        {
        }

        public CharacterWithPerformer(Character c)
        {
            this.Id = c.Id;
            this.ProjectId = c.ProjectId;
            this.Name = c.Name;
            this.PerformerPersonId = c.PerformerPersonId;
        }

        public Person Performer { get; set; }
    }
}
