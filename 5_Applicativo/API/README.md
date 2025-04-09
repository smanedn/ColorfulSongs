# API Leaderboard

## Endpoints

### URL

localhost:8080/

### GET

#### api/public/user

Serve per ritornare tutti gli utenti nel database in formato JSON

| Key |
| ------ |
| score |
| user_id |
| dungeon_id |

#### api/public/user/{id}

Serve per ritornare tutte le informazioni di un utente specifico

| Key |
| ------ |
| id |
| username |
| password |
| email |


### POST

#### api/public/user

Serve per aggiungere un nuovo utente


| Key |
| ------ |
| username |
| password |
| email |


### PUT

#### api/public/user/{id}

Serve per eseguire un update su un utente specifico

| Key    |
|--------|
| username |
| password |
| email |


### DELETE

#### api/public/user/{id}

Serve eliminare un utente specifico
