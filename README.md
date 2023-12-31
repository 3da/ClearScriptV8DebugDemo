# ClearScriptV8DebugDemo
This project demostrates how you can easily integrate JS engine V8 to embed JS script into C# project.
It also shows possibilities to debug and proxy debug with reverse websocket proxy.

Этот проект демонстрирует то, как можно просто интегрировать JS движок V8 для встраивания JS скриптов в проект на C#.
Здесь также показаны возможности отладки и реализовано проксирование отладки через реверсивный websocket прокси.

## How to run

1. First start ConsoleApp. ConsoleApp starts JS script and V8 engine goes to debug state.

1. Then start WebApp. It will show you link to debug via websocket protocol and link to debug with Google Chrome.

Link to debug with Google Chrome looks so:

<ins>devtools://devtools/bundled/js_app.html?experiments=true&v8only=true&ws=localhost:5259/debug</ins>

It doesn't work for me to open direct link right away. But here is workaround. First open this link:

<ins>devtools://devtools/bundled/js_app.html</ins>

Then open direct link, debug will start.

## Как запускать

1. Вначале запустите ConsoleApp. При этом запустится JS скрипт, а движок перейдёт в режим отдадки.

1. Затем запустите WebApp. При этом будет выведена ссылка для отладки по протоколу websocket и ссылка для отладки через Google Chrome.

Ссылка для отладки через Google Chrome имеет следующий вид:

<ins>devtools://devtools/bundled/js_app.html?experiments=true&v8only=true&ws=localhost:5259/debug</ins>

У меня не работает переход по прямой ссылке сразу. Однако эту проблему можно обойти. Вначале откройте ссылку:

<ins>devtools://devtools/bundled/js_app.html</ins>

Затем откройте прямую ссылку, отладка запустится.


## Useful links. Полезные ссылки
- https://github.com/microsoft/ClearScript
- https://github.com/microsoft/ClearScript/discussions/243
- https://microsoft.github.io/ClearScript/Details/Build.html
- https://chromedevtools.github.io/devtools-protocol/
