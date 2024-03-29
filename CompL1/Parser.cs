using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompL1
{
    enum States
    {
        None,
        ENUM,
        CLASS,
        ENUM_ID,
        OPEN_BRACE,
        ID,
        COMMA,
        EQUALS,
        NUMBER,
        CLOSE_BRACE,
        END,
        ERROR
    };

    internal class Parser
    {
        public States curState;
        public Parser()
        {
            curState = States.None;
        }

        public States Parserr(Token token)
        {
            TokenType type = token.Type;
            if (type == TokenType.TOKEN_WHITESPACE);
            else if (curState == States.None && type == TokenType.TOKEN_ENUM) curState = States.ENUM;
            else if (curState == States.ENUM && type == TokenType.TOKEN_CLASS) curState = States.CLASS;
            else if (curState == States.ENUM_ID && type == TokenType.TOKEN_SEMICOLON) curState = States.None;
            else if ((curState == States.ENUM && type == TokenType.TOKEN_IDENT) || (curState == States.CLASS && type == TokenType.TOKEN_IDENT)) curState = States.ENUM_ID;
            else if (curState == States.ENUM_ID && type == TokenType.TOKEN_OPEN_BRACE) curState = States.OPEN_BRACE;
            else if ((curState == States.OPEN_BRACE || curState == States.ID || curState == States.NUMBER) && type == TokenType.TOKEN_CLOSE_BRACE) curState = States.CLOSE_BRACE;
            else if ((curState == States.ID || curState == States.NUMBER) && type == TokenType.TOKEN_COMMA) curState = States.COMMA;
            else if ((curState == States.OPEN_BRACE || curState == States.COMMA) && type == TokenType.TOKEN_IDENT) curState = States.ID;
            else if (curState == States.ID && type == TokenType.TOKEN_EQUALS) curState = States.EQUALS;
            else if (curState == States.EQUALS && type == TokenType.TOKEN_NUMBER) curState = States.NUMBER;
            else if (curState == States.ENUM_ID && type == TokenType.TOKEN_CLOSE_BRACE) curState = States.CLOSE_BRACE;
            else if (curState == States.CLOSE_BRACE && type == TokenType.TOKEN_SEMICOLON) curState = States.None;
            else curState = States.ERROR;

            return curState;
        }
    }
}
