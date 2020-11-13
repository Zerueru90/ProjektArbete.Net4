using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class ExceptionHandling : Exception
    {
        public class EmptyTextBoxException : Exception
        {
            readonly string message = "Tom textbox";
            public override string ToString() => message;
        }

        public class RowSelectionException : Exception
        {
            readonly string message = "Välj en rad";
            public override string ToString() => message;
        }

        //SignIn and Update
        public class EmailFormatException : Exception
        {
            readonly string emailFormat = "Fel email format";
            public override string ToString() => emailFormat;
        }

        public class PasswordFormat : Exception
        {
            readonly string shortPassword = "Lösenordet måste minst ha en stor bokstav och en siffra";
            public override string ToString() => shortPassword;
        }

        public class BirthDateFormatException : Exception
        {
            readonly string date = "Birthdate can only contain numbers and '-' as seperator";
            public override string ToString() => date;
        }

        public class NameFormatException : Exception
        {
            readonly string name = "Kan bara innehålla bokstäver";
            public override string ToString() => name;
        }

        public class NumbersOnlyException : Exception
        {
            readonly string nonNumbers = "Nummer bara";
            public override string ToString() => nonNumbers;
        }
    }
}
