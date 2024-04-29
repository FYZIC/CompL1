using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum State
{
    STATE_INIT,
    STATE_ENUM,
    STATE_CLASS,
    STATE_ENUM_ID,
    STATE_OPEN_BRACE,
    STATE_IDENT,
    STATE_EQUALS,
    STATE_NUMBER,
    STATE_COMMA,
    STATE_CLOSE_BRACE,
    STATE_SEMICOLON,
    STATE_NEWLINE,
    STATE_ERROR
};

public class ParseResult
{
    Token awaited;
    Token actual;
    public bool is_error = false;
    int line;

    public ParseResult(Token awaited, Token actual, int line)
    {
        this.awaited = awaited;
        this.actual = actual;
        if (awaited.type != actual.type)
            this.is_error = true;
        this.line = line;
    }
    public string Stringize(string expr)
    {
        string ret = "Ошибка: line: " + line + " ожидалось: " + this.awaited.type + ", текущий: " + "'" + this.actual.value + "'\n";

        return ret;
    }
    public string actualValue()
    {
        return this.actual.value;
    }
};

public class Parser
{
    public State state;
    int line = 1;

    public ParseResult parse(Token token)
    {
        Token awaited = new Token();
        if (token.type == TokenType.NEWLINE)
            line++;
        // <STATE_INIT> -> ENUM_KEYWORD <STATE_ENUM>
        if (state == State.STATE_INIT)
        {
            awaited.type = TokenType.ENUM_KEYWORD;
            if (token.type == TokenType.ENUM_KEYWORD)
            {
                state = State.STATE_ENUM;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_ENUM> -> CLASS_KEYWORD <STATE_CLASS>
        if (state == State.STATE_ENUM)
        {
            awaited.type = TokenType.CLASS_KEYWORD;
            if (token.type == awaited.type)
            {
                state = State.STATE_CLASS;
                return new ParseResult(awaited, token, line);
            }
        }

        if (state == State.STATE_ENUM)
        {
            awaited.type = TokenType.IDENT;
            if (token.type == awaited.type)
            {
                state = State.STATE_ENUM_ID;
                return new ParseResult(awaited, token, line);
            }

        }
        // <STATE_CLASS> -> IDENT <STATE_ENUM_ID>
        if (state == State.STATE_CLASS)
        {
            awaited.type = TokenType.IDENT;
            if (token.type == awaited.type)
            {
                state = State.STATE_ENUM_ID;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_ENUM_ID> -> OPEN_BRACE <STATE_OPEN_BRACE>
        if (state == State.STATE_ENUM_ID)
        {
            awaited.type = TokenType.OPEN_BRACE;
            if (token.type == awaited.type)
            {
                state = State.STATE_OPEN_BRACE;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_ENUM_ID> -> SEMICOLON <STATE_SEMICOLON>
        if (state == State.STATE_ENUM_ID)
        {
            awaited.type = TokenType.SEMICOLON;
            if (token.type == awaited.type)
            {
                state = State.STATE_SEMICOLON;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_OPEN_BRACE> -> CLOSE_BRACE <STATE_CLOSE_BRACE>
        if (state == State.STATE_OPEN_BRACE)
        {
            awaited.type = TokenType.CLOSE_BRACE;
            if (token.type == awaited.type)
            {
                state = State.STATE_CLOSE_BRACE;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_IDENT> -> CLOSE_BRACE <STATE_CLOSE_BRACE>
        if (state == State.STATE_IDENT)
        {
            awaited.type = TokenType.CLOSE_BRACE;
            if (token.type == awaited.type)
            {
                state = State.STATE_CLOSE_BRACE;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_IDENT> -> COMMA <STATE_COMMA>
        if (state == State.STATE_IDENT)
        {
            awaited.type = TokenType.COMMA;
            if (token.type == awaited.type)
            {
                state = State.STATE_COMMA;
                return new ParseResult(awaited, token, line);
            }
        }
        // <STATE_IDENT> -> EQUALS <STATE_EQUALS>
        if (state == State.STATE_IDENT)
        {
            awaited.type = TokenType.EQUALS;
            if (token.type == awaited.type)
            {
                state = State.STATE_EQUALS;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_EQUALS> -> NUMBER <STATE_NUMBER>
        if (state == State.STATE_EQUALS)
        {
            awaited.type = TokenType.NUMBER;
            if (token.type == awaited.type)
            {
                state = State.STATE_NUMBER;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_NUMBER> -> CLOSE_BRACE <STATE_CLOSE_BRACE>
        if (state == State.STATE_NUMBER)
        {
            awaited.type = TokenType.CLOSE_BRACE;
            if (token.type == awaited.type)
            {
                state = State.STATE_CLOSE_BRACE;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_NUMBER> -> COMMA <STATE_CLOSE_BRACE>
        if (state == State.STATE_NUMBER)
        {
            awaited.type = TokenType.COMMA;
            if (token.type == awaited.type)
            {
                state = State.STATE_COMMA;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_COMMA> -> IDENT <STATE_IDENT>
        if (state == State.STATE_COMMA)
        {
            awaited.type = TokenType.IDENT;
            if (token.type == awaited.type)
            {
                state = State.STATE_IDENT;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_OPEN_BRACE> -> IDENT <STATE_IDENT>
        if (state == State.STATE_OPEN_BRACE)
        {
            awaited.type = TokenType.IDENT;
            if (token.type == awaited.type)
            {
                state = State.STATE_IDENT;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_SEMICOLON> -> ENUM_KEYWORD <STATE_ENUM>
        if (state == State.STATE_SEMICOLON)
        {

            awaited.type = TokenType.ENUM_KEYWORD;
            if (token.type == awaited.type)
            {
                state = State.STATE_ENUM;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_SEMICOLON> -> NEWLINE <STATE_SEMICOLON>
        if (state == State.STATE_SEMICOLON)
        {
            awaited.type = TokenType.NEWLINE;
            if (token.type == awaited.type)
            {
                state = State.STATE_SEMICOLON;
                return new ParseResult(awaited, token, line);
            }
        }

        // <STATE_CLOSE_BRACE> -> SEMICOLON <STATE_SEMICOLON>
        if (state == State.STATE_CLOSE_BRACE)
        {
            awaited.type = TokenType.SEMICOLON;
            if (token.type == awaited.type)
            {
                state = State.STATE_SEMICOLON;
                return new ParseResult(awaited, token, line);
            }
        }

        return new ParseResult(awaited, token, line);
    }
};


/* <DEF> -> 'enum' <ENUM>
<ENUM> -> letter(digit|letter|_) <ENUM_ID>
<ENUM> -> ['class']  <CLASS>
<CLASS> -> letter(digit|letter|_) <ENUM_ID>
<ENUM_ID> -> '{' <OPEN_BRACE>
<ENUM_ID> -> ';' <SEMICOLON>
<OPEN_BRACE> -> letter(digit|letter|_) <ID>
<OPEN_BRACE> -> '}' <CLOSE_BRACE>
<ID> -> '}' <CLOSE_BRACE>
<ID> -> '=' <EQUAL>
<EQUAL> -> digit(digit) <NUMBER>
<ID> -> ',' <COMMA>
<COMMA> -> letter(digit|letter|_) <ID>
<NUMBER> -> '}' <CLOSE_BRACE>
<NUMBER> -> ',' <COMMA>
<CLOSE_BRACE> -> ';' <SEMICOLON>
<SEMICOLON> -> <END>*/