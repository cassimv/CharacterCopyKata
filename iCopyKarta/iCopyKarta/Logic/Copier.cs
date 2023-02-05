using iCopyKarta.Contracts;

namespace iCopyKarta.Logic
{
    public class Copier
    {
        private readonly ISource _source;
        private readonly IDestination _destination;

        public Copier(ISource source, IDestination destination)
        {
            _source = source;
            _destination = destination;
        }

        public void Copy()
        {
            //Read the Source Character
            var readCharacter = _source.ReadChar();
            //Only write to the destination if the source is not a new line character
            if (!readCharacter.Equals('\n'))
                _destination.WriteChar(readCharacter);
        }

        public void CopyCharacters(int noOfCharacters)
        {
            var readCharacter = _source.ReadChars(noOfCharacters);
            //Check to see if the characters have a new line
            var positionOfNewLine = Array.IndexOf(readCharacter, '\n');
            
            //Resize the array and remove the new line
            if (positionOfNewLine != -1)
            {
                Array.Resize<char>(ref readCharacter,positionOfNewLine);
            }
            
            _destination.WriteChars(readCharacter);
        }
    }
}
