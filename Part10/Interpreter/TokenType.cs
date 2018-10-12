namespace Interpreter
{
    public enum TokenType
    {
        INTEGER,
        INTEGER_CONST,
        REAL,
        REAL_CONST,
        PLUS,
        MINUS,
        MUL,
        INTEGER_DIV,
        FLOAT_DIV,
        LPAREN,
        RPAREN,
        ID,
        ASSIGN,
        BEGIN,
        END,
        SEMI,
        DOT,
        PROGRAM,
        VAR,
        COLON,
        COMMA,
        EOF,
    }
}