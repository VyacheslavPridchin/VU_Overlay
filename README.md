#### Not maintained by developers. Не поддерживается разработчиками.
# VU_Overlay
Launcher version for Virtual University. Main functions
  - Start virtual environment by relative path
  - MySQL based account system
  - Audio and video communications
  - Implementation of a system of access levels for student and teacher
  - Creation of a lobby for lessons

To implement video and audio communication, you need to deploy the server:
https://github.com/VyacheslavPridchin/VU_CommunicationServer

To synchronize the account system, you need to deploy a MySQL server with the structure specified in the file in the following repository (can be imported):
https://github.com/VyacheslavPridchin/VU_SQLDatabase

After specifying connection data in SQL.cs scripts on the server and client

To send letters with a confirmation code, you need to deploy a mail server and enter your data to enter the mail service in the Recovery.cs and EnterCode.cs scripts

***

# VU_Overlay
Версия лаунчера для Виртуального университета. Основные функции
  - Запуск виртуальной среды по относительному пути
  - Система аккаунтов на основе MySQL
  - Аудио и видео виды связи
  - Реализация системы уровней доступа для студента и преподавателя
  - Создание лобби для проведения пар

Для осуществления видео и аудио связи необходимо развернуть сервер:
https://github.com/VyacheslavPridchin/VU_CommunicationServer

Для синхронизации системы аккаунтов необходимо развернуть MySQL сервер с структурой, указанной в файле в следующем репозитории (можно импортировать):
https://github.com/VyacheslavPridchin/VU_SQLDatabase

После в скриптах SQL.cs на сервере и клиенте указать данные для подключения

Для отправки писем с кодом подтверждения необходимо развернуть почтовый сервер и в скриптах Recovery.cs и EnterCode.cs ввести свои данные для входа в почтовый сервис
