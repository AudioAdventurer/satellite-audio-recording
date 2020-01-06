using SAR.Modules.Script.Objects;

namespace SAR.Apps.Server.Objects
{
    public class UICharacter : Character
    {
        public UICharacter()
        {
        }

        public UICharacter(Character c)
        {
            this.Id = c.Id;
            this.ProjectId = c.ProjectId;
            this.Name = c.Name;
            this.PerformerPersonId = c.PerformerPersonId;
            this.FirstDialogSequenceNumber = c.FirstDialogSequenceNumber;
            this.LastDialogSequenceNumber = c.LastDialogSequenceNumber;
        }

        public Person Performer { get; set; }
    }
}
