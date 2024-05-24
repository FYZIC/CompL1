# Лабораторная работа 6
## Описание

Цель работы: Реализовать алгоритм поиска в тексте подстрок, соответствующих заданным регулярным выражениям

## Задачи
### Первый блок
19. Построить РВ, описывающее время в формате HH:MM:SS.
### Второй блок
24. Построить РВ для поиска ИНН физических и юридических
лиц.
### Третий блок
2. Построить РВ, описывающее IP-адрес (v4). Учесть корректные
значения, например, порты находятся в диапазоне от 0 до 65535.

### Решение РВ

1) [0-1][0-9]:[0-5][0-9]:[0-5][0-9]  || [2][0-3]:[0-5][0-9]:[0-5][0-9]

2) [0-9]{12} || [0-9]{10}

3) ([0-9]{1,3}.){3}[0-9]{1,3}:(0|[1-9][0-9]{0,3}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])?

## Тестовые примеры
### Для первого РВ
![Схема](6Пример.png)

![Схема](6Пример2.png)

### Для второго РВ
![Схема](6Пример3.png)

![Схема](6Пример4.png)

### Для третьего РВ
![Схема](6Пример5.png)

![Схема](6Пример6.png)

![Схема](6Пример7.png)
