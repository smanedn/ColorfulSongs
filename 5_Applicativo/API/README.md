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

Serve per ritornare tutte le informazioni di un utente specifico, le informazioni ritornate sono queste:

| Key |
| ------ |
| id |
| username |
| password |
| email |
|type|


### POST

#### /user

Serve per aggiungere un nuovo utente o un nuovo punteggio

Per l'aggiunta di un nuovo utente bisogna inserire:

| Key |
| ------ |
| username |
| password |
| email |

Per l'aggiunta di un nuovo punteggio bisogna inserire:

| Key        |
|------------|
| score      |
| user_id    |

### PUT

#### /user/{id}

Serve per eseguire l'update del punteggio di un utente <br>
Per updateare un utente bisogna inserire:

| Key      |
|----------|
| score    |
| user_id |


### DELETE

#### /user/{id}

Serve a eliminare un utente specifico
