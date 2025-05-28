using System.Text;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class API_CALL : MonoBehaviour
{
    private string URL = "localhost:8080/API/public";
    private void Start()
    {
        StartCoroutine(addUser("SamTrevano"));
    }

    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null)
        {
            //Se c'è un data, faccio un encoding to JSON del data
            //Si vede a riga 24 un esempio
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            //faccio un upload del bodyRaw
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }

    private IEnumerator getUser(String username)
    {
        String url = URL + "/user/" + username;
        var getRequest = CreateRequest(url); //-> Creo richiesta all'URL, vedere meglio funzione
        yield return getRequest.SendWebRequest(); //-> Attendo una risposta dalla richiesta

        string text = getRequest.downloadHandler.text;
        text = text.Replace("[", "");
        text = text.Replace("]", "");

        if (text != null)
        {
            User user = JsonUtility.FromJson<User>(text); //-> deserializzo il json
            PlayerPrefs.SetInt("userId", user.id);
            PlayerPrefs.SetString("userName", user.username);
            PlayerPrefs.Save();

            yield return user;
        }
        else
        {
            yield return null;
        }

    }

    //FUNZIONA
    private IEnumerator addUser(string _username)
    {
        if (getUser(_username) != null)
        {
            UserToAdd user = new UserToAdd() { username = _username, password = "PasswordSicura", email = "www.samtrevano@sam.ch" };
            var postRequest = CreateRequest(URL + "/user", RequestType.POST, user);
            yield return postRequest.SendWebRequest();

            // Gestione della risposta POST
            if (postRequest.result != UnityWebRequest.Result.Success)
            {
                yield break;
            }
            StartCoroutine(getUser(user.username));
        }
    }

    public IEnumerator addScore()
    {
        ScoreAPI scoreAPI = new ScoreAPI() { score = PlayerPrefs.GetString("score"), user_id = PlayerPrefs.GetInt("userId") };
        var postRequest = CreateRequest(URL + "/user", RequestType.POST, scoreAPI);
        yield return postRequest.SendWebRequest();

        // Gestione della risposta POST
        if (postRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("POST request failed: " + postRequest.error);
            yield break;
        }
    }

    private IEnumerator updateScore(ScoreAPI _scoreApi)
    {
        int id = _scoreApi.user_id;
        String _url = URL + "/user/" + id;
        var putRequest = CreateRequest(_url, RequestType.PUT, _scoreApi);
        yield return putRequest.SendWebRequest();

        // Gestione della risposta PUT
        if (putRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("PUT request failed: " + putRequest.error);
            yield break;
        }
    }
}

public enum RequestType
{
    GET = 0,
    POST = 1,
    PUT = 2
}



public class User
{
    public int id;
    public string username;
    public string password;
    public string email;
    public string type;
}

public class UserToAdd
{
    public string username;
    public string password;
    public string email;
}

public class ScoreAPI
{
    public int user_id;
    public String score;
}

[Serializable]
public class PostData
{
    public string username;
    public string password;
    public string email;
}

public class PostResult
{
    public string success { get; set; }
}