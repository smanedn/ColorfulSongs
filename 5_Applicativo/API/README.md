# API Leaderboard

## Endpoints

### URL

localhost:8080/API/public/

### GET

#### /user/{username}

Ritrona l'utente specifico in formato JSON

| Key      |
|----------|
| id       |
| username |
| password |

#### /user/{id}

Serve per ritornare tutte le informazioni di un utente specifico

| Key |
| ------ |
| id |
| username |
| password |
| email |


### POST

#### /user

Serve per aggiungere un nuovo utente o un nuovo punteggio

Per l'aggiunta di un nuovo utente:

| Key |
| ------ |
| username |
| password |
| email |

Per l'aggiunta di un nuovo punteggio:

| Key        |
|------------|
| score      |
| user_id    |
| dungeon_id |

### PUT

#### /user/{id}

Serve per eseguire l'update del punteggio di un untente

| Key      |
|----------|
| score    |
| user_id |
| dungeon_id    |


### DELETE

#### /user/{id}

Serve a eliminare un utente specifico
