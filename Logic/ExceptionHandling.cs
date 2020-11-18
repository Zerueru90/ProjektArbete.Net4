using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class ExceptionHandling : Exception
    {
        public class EmptyTextBoxException : Exception
        {
            readonly string message = "Du måste fylla i alla uppgifter";
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
            readonly string emailFormat = "Du måste ha med @ i din mailadress";
            public override string ToString() => emailFormat;
        }

        public class PasswordFormat : Exception
        {
            readonly string shortPassword = "Lösenordet måste minst ha en stor bokstav och en siffra";
            public override string ToString() => shortPassword;
        }

        public class NameFormatException : Exception
        {
            readonly string name = "Kan bara innehålla bokstäver";
            public override string ToString() => name;
        }

        public class NumbersOnlyException : Exception
        {
            readonly string nonNumbers = "Mätarställning är felaktigt ifyllt";
            public override string ToString() => nonNumbers;
        }
        public class RegNumberException : Exception
        {
            readonly string regnumber = "Regnummber måste innehålla tre siffror och tre nummer";
            public override string ToString() => regnumber;
        }

        public class MaxLoadWeight : Exception
        {
            readonly string numberDecimal = "Lastvikt är felaktigt ifyllt";
            public override string ToString() => numberDecimal;
        }

        public class MaxPassangersException : Exception
        {
            readonly string numberDecimal = "Max passagerare är felaktigt ifyllt";
            public override string ToString() => numberDecimal;
        }

    }
}
