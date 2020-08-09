namespace AutomateBase
{
    public class Ticket
    {
        public char Letter { get; private set; }

        public Ticket(char letter)
        {
            Letter = letter;
        }

        public bool IsLetter(char letter)
        {
            return Letter == letter;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Ticket ticket))
            {
                return false;
            }

            return ticket.Letter.Equals(Letter);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
