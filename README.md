* [К оглавлению задачника](https://github.com/urfu-code/cs101-main)
* [Кодекс разработчика](https://docs.google.com/document/d/1w8C1VyDPh9_1DaGD6oDJWmHw8V6cWrr469CgMiLGmdE/edit#)

Задачи на поиск
===

Поиск корня
----

Поиск корней полиномов — очень важная задача. 
Об этом говорит хотя бы то, что [Основная теорема алгебры](http://ru.wikipedia.org/wiki/%D0%9E%D1%81%D0%BD%D0%BE%D0%B2%D0%BD%D0%B0%D1%8F_%D1%82%D0%B5%D0%BE%D1%80%D0%B5%D0%BC%D0%B0_%D0%B0%D0%BB%D0%B3%D0%B5%D0%B1%D1%80%D1%8B) посвящена именно корням полинома.
Однако локализованный корень очень легко найти приближенно для любой непрерывной функции, в том числе и для полинома.

Вам нужно написать консольную программу, которая получает на вход данные из параметров командной строки (массив args в функции Main).
Программа должна найти корни полинома с некоторой точностью с заданными коэффициентами на указанном отрезке.

Ваша программа должна запускаться следующим образом:

```
root.exe <left> <right> <a0> <a1> <a2> ... <aN>
```

* Она должна выводить решение уравнения `f(x) = 0`, где `f(x) = a0 + a1*x + ... + aN*x^N` на отрезке `left..right`, с точностью `1e-6`.
* Если `f(left)` и `f(right)` имеют одинаковый знак, то программа должна выйти сообщив об этом, без поиска решения.
* Метод поиска корня должен быть реализован с помощью рекурсии.

Узнать больше про численные методы поиска корней функции можно узнать из википедии. Начать можно отсюда: [Root-finding_algorithm](http://en.wikipedia.org/wiki/Root-finding_algorithm)


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


