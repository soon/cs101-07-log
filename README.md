* [К оглавлению задачника](https://github.com/urfu-code/cs101-main)
* [Кодекс разработчика](https://docs.google.com/document/d/1w8C1VyDPh9_1DaGD6oDJWmHw8V6cWrr469CgMiLGmdE/edit#)

Задачи на поиск
===

Autocomplete
---

Во многих программах в разных контекстах можно увидеть функцию автодополнения вводимого текста.
Автодополнение обычно работает так: есть какой-то готовый словарь, содержащий все значения, 
которые автодополнение может подсказывать, а когда пользователь вводит начало некоторого слова — ему 
показывают несколько подходящих слов из словаря, начинающиеся с букв, уже введенных пользователем.

Такую функцию очень просто реализовать "в лоб", если словарь небольшой. 
Если же словарь большой, то необходимо задумываться об эффективности алгоритма.

* Распакуйте файл со словарем words.zip
* Запустите проект autocomplete и поизучайте программу. В частности попробуйте набрать ZZZ.RU, либо какую-нибудь случайную комбинацию букв.
* В проекте autocomplete выполните все задания, написанные в комментариях в файле [Autocompleter.cs](autocomplete/Autocompleter.cs)

В этой задаче нет автоматических тестов, проверяющих ваше решение. Однако в этой задаче есть множество мест, где можно ошибиться.
Придумайте какой-нибудь способ проверить корректность вашего решения.


Root
----

Вам нужно написать консольную программу, которая получает на вход данные из параметров командной строки (массив args в функции Main).
Программа должна найти корни полинома с некоторой точностью с заданными коэффициентами на указанном отрезке.

Ваша программа должна запускаться следующим образом:

```
root.exe <left> <right> <a0> <a1> <a2> ... <aN>
```

* Она должна выводить решение уравнения `f(x) = 0`, где `f(x) = a0 + a1*x + ... + aN*x^N` на отрезке `left..right`, с точностью `1e-6`.
* Если `f(left)` и `f(right)` имеют одинаковый знак, то программа должна выйти сообщив об этом, без поиска решения.
* Метод поиска корня должен быть реализован с помощью рекурсии.
