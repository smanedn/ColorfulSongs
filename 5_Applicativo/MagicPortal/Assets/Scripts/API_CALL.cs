using System.Text;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class API_CALL : MonoBehaviour
{
    private string URL = "localhost:8080/API/public";
    public string username;
    private void Start()
    {
        //UserToAdd user = new UserToAdd() { username = "userDiProva", password = "sonoUnaBanana", email = "www.megaemail@gmail.it" };
        TimeSpan _score = new TimeSpan(1, 1, 1);
        StartCoroutine(addUser(user));
    }

    private IEnumerator MakeRequests()
    {

        var userToPost = new UserToAdd() { username = "SebastianoFigo", password = "sonoUnaBanana", email = "www.email@gmail.it" };
        string jsonUser = JsonUtility.ToJson(userToPost);
        Debug.Log(jsonUser);

        var postRequest = CreateRequest(URL + "/user", RequestType.POST, userToPost);
        yield return postRequest.SendWebRequest();

        // Gestione della risposta POST
        if (postRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("POST request failed: " + postRequest.error);
            yield break;
        }

        // GET
        var getRequest = CreateRequest(URL + "/user/SebastianoFigo"); //-> Creo richiesta all'URL, vedere meglio funzione
        yield return getRequest.SendWebRequest(); //-> Attendo una risposta dalla richiesta

        string text = getRequest.downloadHandler.text;
        text = text.Replace("[", "");
        text = text.Replace("]", "");

        User user = JsonUtility.FromJson<User>(text); //-> deserializzo il json
        Debug.Log("\nId: " + user.id + "\nusername: " + user.username);
        //FINO QUI OKAY
    }


    //path = URL,   type=GET/POST,  data = in caso di post
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

    //FUNZIONA
    private IEnumerator addUser(UserToAdd user)
    {
        var postRequest = CreateRequest(URL + "/user", RequestType.POST, user);
        yield return postRequest.SendWebRequest();

        // Gestione della risposta POST
        if (postRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("POST request failed: " + postRequest.error);
            yield break;
        }
    }

    private IEnumerator addScore(User _user, TimeSpan _score)
    {
        int _id = _user.id;
        Score score = new Score() { score = _score, id = _id };
        var postRequest = CreateRequest(URL + "/user", RequestType.POST, _user);
        yield return postRequest.SendWebRequest();

        // Gestione della risposta POST
        if (postRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("POST request failed: " + postRequest.error);
            yield break;
        }
    }

    private IEnumerator getUser(String username)
    {
        String url = URL + "/user/" + username;
        var getRequest = CreateRequest(url); //-> Creo richiesta all'URL, vedere meglio funzione
        yield return getRequest.SendWebRequest(); //-> Attendo una risposta dalla richiesta

        string text = getRequest.downloadHandler.text;
        text = text.Replace("[", "");
        text = text.Replace("]", "");

        User user = JsonUtility.FromJson<User>(text); //-> deserializzo il json
        yield return user;
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
    // Ensure no getters / setters
    // Typecase has to match exactly
    public int id;
    public string username;
    public string password;
    public string email;
    public string type;
}

public class UserToAdd
{
    // Ensure no getters / setters
    // Typecase has to match exactly
    public string username;
    public string password;
    public string email;
}

public class Score
{
    public int id;
    public TimeSpan score;
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
