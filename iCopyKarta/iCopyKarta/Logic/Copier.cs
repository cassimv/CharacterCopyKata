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
            var readCharacter = _source.ReadChar();
            if (!readCharacter.Equals('\n'))
                _destination.WriteChar(readCharacter);
        }

        public void CopyCharacters(int noOfCharacters)
        {
            var readCharacter = _source.ReadChars(noOfCharacters);
            var positionOfNewLine = Array.IndexOf(readCharacter, '\n');
            
            if (positionOfNewLine != -1)
            {
                Array.Resize<char>(ref readCharacter,positionOfNewLine);
            }
            
            _destination.WriteChars(readCharacter);
        }
    }
}
