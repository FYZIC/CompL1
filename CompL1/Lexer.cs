
using System;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace CompL1
{
    internal class Lexer
    {
        static Token lexer(string strToLex)
        {
            switch (strToLex)
            {
                case "enum": return new Token("ключевое слово", TokenType.TOKEN_ENUM);
                case "class": return new Token("ключевое слово", TokenType.TOKEN_CLASS);
                case "=": return new Token("оператор присваивания", TokenType.TOKEN_EQUALS);
                case ";": return new Token("конец оператора", TokenType.TOKEN_SEMICOLON);
                case "{": return new Token("открывающая фигурная скобка", TokenType.TOKEN_OPEN_BRACE);
                case "}": return new Token("закрывающая фигурная скобка", TokenType.TOKEN_CLOSE_BRACE);
                case ",": return new Token("запятая", TokenType.TOKEN_COMMA);
                case " ":
                case "\n":
                case "\t":
                    return new Token("разделитель", TokenType.TOKEN_WHITESPACE);
                case "":
                default: break;
            }
            Regex ident = new Regex("[A-Za-z_]([A-Za-z_]|[0-9])*");
            Regex number = new Regex("[0-9]+");

            if (ident.IsMatch(strToLex))
            {
                Match match = ident.Match(strToLex);
                string str = match.Value;
                return new Token("идентификатор", TokenType.TOKEN_IDENT);
            }


            if (number.IsMatch(strToLex))
            {
                Match match = number.Match(strToLex);
                string str = match.Value;
                return new Token("число", TokenType.TOKEN_NUMBER);
            }
            return new Token("недопустимый символ", TokenType.TOKEN_ERROR);
        }

        public static string lexText(string text)
        {
            Parser parser = new Parser();
            string temp = string.Empty;
            int curLine = 0;
            int startPos = 0;
            int endPos = 0;
            string finalText = string.Empty;

            if (text == "")
                return "";

            Token curToken = lexer(text[0].ToString());
            Token tempToken;
            int countErr = 0;

            for (int i = 0; i < text.Length; i++)
            {
                tempToken = lexer(text[i].ToString());

                if (curToken.Type == TokenType.TOKEN_IDENT && tempToken.Type == TokenType.TOKEN_ERROR)
                {
                    tempToken = curToken;
                }

                if (tempToken.Type != curToken.Type)
                {
                    curToken = lexer(temp);

                    endPos--;

                    if (parser.Parserr(curToken) == States.ERROR)
                    {
                        finalText += "Ошибка: " + curToken.Type + " - " + temp + " - " + " position" +
                          " [" + startPos + "," + endPos + "]" + " line: " + curLine + "\n";
                        countErr++;
                    }

                    if (temp == "\n")
                    {
                        curLine++;
                        startPos = 0;
                        endPos = 0;
                        temp = string.Empty;
                        curToken = tempToken;
                    }
                    else
                    {
                        endPos++;
                        startPos = endPos;
                        temp = string.Empty;
                        curToken = tempToken;
                    }
                }

                temp += text[i];
                endPos++;
            }
            curToken = lexer(temp);
            endPos--;

            if (countErr == 0)
                finalText += "Ошибок нет";
            else
                finalText += "Всего ошибок: " + countErr;

            return finalText;


        }

        public static string QuickFixErrors(string text)
        {
            Parser parser = new Parser();
            string temp = string.Empty;
            int curLine = 0;
            int startPos = 0;
            int endPos = 0;
            string finalText = string.Empty;

            if (text == "")
                return "";

            Token curToken = lexer(text[0].ToString());
            Token tempToken;
            int countErr = 0;

            for (int i = 0; i < text.Length; i++)
            {
                tempToken = lexer(text[i].ToString());

                if (curToken.Type == TokenType.TOKEN_IDENT && tempToken.Type == TokenType.TOKEN_ERROR)
                {
                    tempToken = curToken;
                }

                if (tempToken.Type != curToken.Type)
                {
                    curToken = lexer(temp);

                    endPos--;

                    if (parser.Parserr(curToken) != States.ERROR)
                    {
                        finalText += temp;
                    }

                    if (temp == "\n")
                    {
                        temp = string.Empty;
                        curToken = tempToken;
                    }
                    else
                    {
                        temp = string.Empty;
                        curToken = tempToken;
                    }
                }

                temp += text[i];
            }
            curToken = lexer(temp);

            return finalText;


        }
    }

    enum TokenType
    {
        TOKEN_ENUM = 1,
        TOKEN_CLASS,
        TOKEN_IDENT,
        TOKEN_WHITESPACE,
        TOKEN_EQUALS,
        TOKEN_SEMICOLON,
        TOKEN_OPEN_BRACE,
        TOKEN_CLOSE_BRACE,
        TOKEN_COMMA,
        TOKEN_NUMBER,
        TOKEN_ERROR,
    };

    internal class Token
    {
        string name;
        TokenType type;
        public Token(string name, TokenType type)
        {
            this.name = name;
            this.type = type;
        }

        public string Name { get { return name; } set { name = value; } }
        public TokenType Type { get { return type; } set { type = value; } }
    }
}

/* <DEF> -> 'enum' -> <CLASS>
<DEF> -> 'enum' -> <ENUM_ID>
<CLASS> -> 'class' -> <ENUM_ID>
<ENUM_ID> -> (letter | _) -> <ENUM_IDREM>
<ENUM_IDREM> -> (digit|letter|_) -><ENUM_IDREM>
<ENUM_IDREM> -> '{' <OPEN_BRACE>
<ENUM_IDREM> -> <END>
<OPEN_BRACE> -> <ID>
<OPEN_BRACE> -> '}' <END>
<ID> -> (letter | _) <IDREM>
<IDREM> -> (digit|letter|_) <IDREM>
<IDREM> -> '}' <END>
<IDREM> -> '=' <NUMBER>
<IDREM> -> ',' <ID>
<NUMBER> -> digit <NUMBER_REM>
<NUMBER_REM> -> digit <NUMBER_REM>
<NUMBER_REM> -> '}' <END>
<NUMBER_REM> -> ',' <ID> 
<END> ; -> */

/* <DEF> -> 'enum' <ENUM>
<ENUM> -> (digit|letter|_) <ENUM_ID>
<ENUM> -> 'class'  <CLASS>
<CLASS> -> (digit|letter|_) <ENUM_ID>
<ENUM_ID> -> '{' <OPEN_BRACE>
<ENUM_ID> -> ';' <SEMICOLON>
<OPEN_BRACE> -> (digit|letter|_) <ID>
<OPEN_BRACE> -> '}' <CLOSE_BRACE>
<ID> -> '}' <CLOSE_BRACE>
<ID> -> '=' <EQUAL>
<EQUAL> -> digit <NUMBER>
<ID> -> ',' <COMMA>
<COMMA> -> (digit|letter|_) <ID>
<NUMBER> -> '}' <CLOSE_BRACE>
<NUMBER> -> ',' <COMMA> 
<CLOSE_BRACE> -> ';' <SEMICOLON>
<SEMICOLON> -> <END>*/

//enum dfer { };
//enum class dfg { };
//enum dfg2 {esrgdthy };
//enum dfg3 {efrg = 3 };
//enum dfg4 {dfg = 8, rtg };
//enum dffgg;