# Объявление перечисляемого типа на языке C++
## Примеры допустимых строк
```
enum samyil {first = 1, second, third};
enum ff445dd {el1=1, el2=2, el3=3};
```
## Разработанная грамматика
```
<DEF> -> 'enum' <ENUM>
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
<SEMICOLON> -> <END>
```
## Граф конечного автомата
![alt text](граф.PNG)
## Тестовые примеры
![alt text](444.PNG)
![alt text](555.PNG)
