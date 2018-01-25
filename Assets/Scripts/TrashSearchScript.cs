using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrashSearchScript : MonoBehaviour {

    TrashData trash;

    private void Start()
    {
        trash = TrashManager.GetInstance().GetTrash();
        print(trash);
        GameObject.Find("LabelText").GetComponent<Text>().text = trash.TrashName;
    }

    public int captureWidth = 600;
    public int captureHeight = 1024;

    private const string API_KEY = "AIzaSyB3S7o3-A1nKrvfeL4FGG_4S0iTy67tbbg";
    private const string API_URL = "https://vision.googleapis.com/v1/images:annotate?key=";
    public int maxResults = 20;
    Dictionary<string, string> headers;

    byte[] fileData = null;     
    
    // private vars for screenshot
    private Rect rect;
    private RenderTexture renderTexture;
    private Texture2D screenShot;

    [System.Serializable]
    public class AnnotateImageRequests
    {
        public List<AnnotateImageRequest> requests;
    }

    [System.Serializable]
    public class AnnotateImageRequest
    {
        public Image image;
        public List<Feature> features;
    }

    [System.Serializable]
    public class Image
    {        
        public string content;
    }

    [System.Serializable]
    public class Feature
    {
        public string type;
        public int maxResults;
    }

    [System.Serializable]
    public class AnnotateImageResponses
    {
        public List<AnnotateImageResponse> responses;
    }

    [System.Serializable]
    public class AnnotateImageResponse
    {
        public EntityAnnotation webDetection;
    }

    [System.Serializable]
    public class EntityAnnotation
    {
        public List<webEntity> webEntities;
    }

    [System.Serializable]
    public class webEntity
    {
        public string description;
    }

    public void OnClick()
    {
        StartCoroutine(Take());
    }

    public IEnumerator Take()
    {
        Camera camera = GameObject.Find("ARCamera").GetComponent<Camera>();
        GameObject.Find("LabelText").GetComponent<Text>().text = trash.TrashName;
        captureWidth = camera.pixelWidth;
        captureHeight = camera.pixelHeight;

        Destroy(renderTexture);
        renderTexture = null;
        screenShot = null;

        // create screenshot objects if needed
        if (renderTexture == null)
        {
            // creates off-screen render texture that can rendered into
            rect = new Rect(0, 0, captureWidth, captureHeight);
            renderTexture = new RenderTexture(captureWidth, captureHeight, 24);
            screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        }

        // get main camera and manually render scene into rt

        camera.targetTexture = renderTexture;
        camera.Render();

        // read pixels will read from the currently active render texture so make our offscreen 
        // render texture active and then read the pixels
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(rect, 0, 0);

        // reset active camera texture and render texture
        camera.targetTexture = null;
        RenderTexture.active = null;

        fileData = screenShot.EncodeToPNG();

        yield return new WaitForSeconds(1.0f);        
        StartCoroutine("DetectImage");
    }

    public IEnumerator DetectImage()
    {
        headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json; charset=UTF-8");        

        string base64 = System.Convert.ToBase64String(fileData);

        AnnotateImageRequests requests = new AnnotateImageRequests();
        requests.requests = new List<AnnotateImageRequest>();

        AnnotateImageRequest request = new AnnotateImageRequest();

        request.image = new Image();
        request.image.content = base64;        
        request.features = new List<Feature>();

        Feature feature = new Feature();
        feature.type = "WEB_DETECTION";
        feature.maxResults = this.maxResults;

        request.features.Add(feature);

        requests.requests.Add(request);

        string jsonData = JsonUtility.ToJson(requests, false);

        print(jsonData);

        if (jsonData != string.Empty)
        {
            string url = API_URL + API_KEY;
            print(url);
            byte[] postData = System.Text.Encoding.Default.GetBytes(jsonData);
            using (WWW www = new WWW(url, postData, headers))
            {
                yield return www;
                print(www);
                if (string.IsNullOrEmpty(www.error))
                {
                    Debug.Log(www.text.Replace("\n", "").Replace(" ", ""));
                    AnnotateImageResponses responses = JsonUtility.FromJson<AnnotateImageResponses>(www.text);                    
                    Sample_OnAnnotateImageResponses(responses);
                }
                else
                {
                    Debug.Log("Error: " + www.error);
                }
            }
        }        
    }

    void Sample_OnAnnotateImageResponses(AnnotateImageResponses responses)
    {                
        for(int i = 0; i < responses.responses[0].webDetection.webEntities.Count; i++)
        {
            print("Base: " + trash.TrashBase + "Res: " +responses.responses[0].webDetection.webEntities[i].description.ToUpper());
            if ((trash.TrashBase.ToUpper().Contains(responses.responses[0].webDetection.webEntities[i].description.ToUpper()) ||
                trash.TrashName.ToUpper().Contains(responses.responses[0].webDetection.webEntities[i].description.ToUpper())))
            {
                for (int j = 0; j < responses.responses[0].webDetection.webEntities.Count; j++)
                {
                    if(trash.TrashMatComp.ToUpper().Contains(responses.responses[0].webDetection.webEntities[j].description.ToUpper()))
                    {
                        GameObject.Find("LabelText").GetComponent<Text>().text = "TRUE";
                        break;
                    }                   
                }
                GameObject.Find("LabelText").GetComponent<Text>().text = "NOT SURE";
                break;
            }
               
            else
                GameObject.Find("LabelText").GetComponent<Text>().text = "FALSE";
        }
       
        print("END");
    }
}
