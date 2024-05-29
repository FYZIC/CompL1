using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Compl1
{

    public class Parser
    {
        string strToParse;
        int index;
        string result;
        public Parser(string strToParse)
        {
            this.strToParse = strToParse;
            result = string.Empty;
        }

        public string Result { get { return result; } }

        public void Parse()
        {
            index = -1;
            Doc();



        }

        void Doc()
        {
            Token token;
            index++; token = lexer(strToParse[index]);
            result += "Doc-";
            switch (token.Type)
            {
                case TokenType.TOKEN_INSTR:
                case TokenType.TOKEN_CHAR:
                    Element(token);
                    Doc(); break;

                default: result += "e-"; break;
            }

        }

        void Element(Token token)
        {
            result += "Element-";
            if (token.Lexeme == '<' && token.Type == TokenType.TOKEN_INSTR)
            {
                index++; token = lexer(strToParse[index]);
                switch (token.Lexeme)
                {
                    case 'e':
                        index += 2;

                        if ((strToParse[index - 1] + strToParse[index]).ToString() == "m>")
                        {
                            Doc();
                            if (index < strToParse.Length)
                            {
                                if (strToParse[index] == '<')
                                {
                                    index += 4;
                                    if ((strToParse[index - 3] + strToParse[index - 2] + strToParse[index - 1] + strToParse[index]).ToString() == "/em>")
                                    {
                                        break;
                                    }

                                }
                                else
                                {
                                    index--;
                                }
                            }

                        }
                        break;
                    case 'p':
                        index += 1;

                        if ((strToParse[index]).ToString() == ">")
                        {
                            Doc();
                            if (index < strToParse.Length)
                            {
                                if (strToParse[index] == '<')
                                {
                                    index += 3;
                                    if ((strToParse[index - 2] + strToParse[index - 1] + strToParse[index]).ToString() == "/p>")
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    index--;
                                }
                            }

                        }
                        break; // 
                    case 'o':
                        index += 2;

                        if ((strToParse[index - 1] + strToParse[index]).ToString() == "l>")
                        {
                            List();
                            if (index < strToParse.Length)
                            {
                                if (strToParse[index] == '<')
                                {
                                    index += 4;
                                    if ((strToParse[index - 3] + strToParse[index - 2] + strToParse[index - 1] + strToParse[index]).ToString() == "ol>")
                                    {
                                        break;
                                    }

                                }
                                else
                                {
                                    index--;
                                }
                            }

                        }
                        break;
                    default: break;
                }
            }

            if (token.Type == TokenType.TOKEN_CHAR)
            {
                Text();
            }
        }



        void List()
        {
            result += "List-";

            Token token;
            index++; token = lexer(strToParse[index]);
            result += "Doc-";
            switch (token.Type)
            {
                case TokenType.TOKEN_INSTR: ListItem(token); break;

                default: result += "e-"; break;
            }
        }//

        void ListItem(Token token)
        {
            result += "ListItem-";
            if (token.Lexeme == '<')
            {
                index += 3;
                if ((strToParse[index - 2] + strToParse[index - 1] + strToParse[index]).ToString() == "li>")
                {
                    Text();
                    if (index < strToParse.Length)
                    {
                        if (strToParse[index] == '<')
                        {
                            index += 4;
                            if ((strToParse[index - 3] + strToParse[index - 2] + strToParse[index - 1] + strToParse[index]).ToString() == "/li>")
                            {

                            }

                        }
                        else
                        {
                            index--;
                        }
                    }

                }
            }
        }

        void Text()
        {
            Token token;
            index++; token = lexer(strToParse[index]);
            result += "Text-";
            switch (token.Type)
            {
                case TokenType.TOKEN_CHAR:
                    Text(); break;

                default: result += "e-"; break;
            }
        }

        Token lexer(char strToLex)
        {
            Token token;
            switch (strToLex)
            {
                case '<': token = new Token("разделитель", TokenType.TOKEN_INSTR); token.Lexeme = strToLex; return token;
                case ' ': return new Token("разделитель", TokenType.TOKEN_WHITESPACE);
                case '\r': return new Token("разделитель", TokenType.TOKEN_WHITESPACE_R);
                case '\n': return new Token("разделитель", TokenType.TOKEN_WHITESPACE_N);
                default: break;
            }

            if (Char.IsLetter(strToLex))
            {
                token = new Token("разделитель", TokenType.TOKEN_CHAR); token.Lexeme = strToLex; return token;
            }

            return new Token("недопустимый символ", TokenType.TOKEN_ERROR);
        }
    }
}
