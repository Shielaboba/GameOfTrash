/*============================================================================== 
 Copyright (c) 2016-2017 PTC Inc. All Rights Reserved.
 
 Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UDTEventHandler : MonoBehaviour, IUserDefinedTargetEventHandler
{
    // private vars for screenshot
    private Rect rect;
    private RenderTexture renderTexture;
    private Texture2D screenShot;

    public int captureWidth = 600;
    public int captureHeight = 1024;
    byte[] fileData = null;
    public int maxResults = 10;
    Dictionary<string, string> headers;
    Boolean holder = false;
    TrashData trash;

    LifeManager lifeManager;    

    private const string API_KEY = "AIzaSyB3S7o3-A1nKrvfeL4FGG_4S0iTy67tbbg";
    private const string API_URL = "https://vision.googleapis.com/v1/images:annotate?key=";
    private GameObject[] typeBtn = new GameObject[4];
    private GameObject noLifeDetails;
    private Button BackBtn, OkayBtn, BuildBtn;

    #region SERIALIZABLE_CLASS
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
        public List<WebEntity> webEntities;
    }

    [System.Serializable]
    public class WebEntity
    {
        public string description;
    }
    #endregion


    #region PUBLIC_MEMBERS
    /// <summary>
    /// Can be set in the Unity inspector to reference an ImageTargetBehaviour 
    /// that is instantiated for augmentations of new User-Defined Targets.
    /// </summary>
    public ImageTargetBehaviour ImageTargetTemplate;

    public int LastTargetIndex
    {
        get { return (m_TargetCounter - 1) % MAX_TARGETS; }
    }
    #endregion PUBLIC_MEMBERS


    #region PRIVATE_MEMBERS
    const int MAX_TARGETS = 1;
    UserDefinedTargetBuildingBehaviour m_TargetBuildingBehaviour;
    QualityDialog m_QualityDialog;
    ObjectTracker m_ObjectTracker;
    TrackableSettings m_TrackableSettings;
    FrameQualityMeter m_FrameQualityMeter;

    // DataSet that newly defined targets are added to
    DataSet m_UDT_DataSet;

    // Currently observed frame quality
    ImageTargetBuilder.FrameQuality m_FrameQuality = ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE;

    // Counter used to name newly created targets
    int m_TargetCounter;
    #endregion //PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();
        noLifeDetails = GameObject.Find("noLifeDetails");
        BackBtn = GameObject.Find("BackButton").GetComponent<Button>();
        OkayBtn = GameObject.Find("OkayBtn").GetComponent<Button>();
        BuildBtn = GameObject.Find("BuildButton").GetComponent<Button>();
        noLifeDetails.SetActive(false);
        trash = TrashManager.GetInstance().GetTrash();
        ConfigBtn();

        GameObject.Find("Title").GetComponent<Text>().text = trash.TrashName;
        m_TargetBuildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour>();

        if (m_TargetBuildingBehaviour)
        {
            m_TargetBuildingBehaviour.RegisterEventHandler(this);
            Debug.Log("Registering User Defined Target event handler.");
        }

        m_FrameQualityMeter = FindObjectOfType<FrameQualityMeter>();
        m_TrackableSettings = FindObjectOfType<TrackableSettings>();
        m_QualityDialog = FindObjectOfType<QualityDialog>();

        if (m_QualityDialog)
        {
            m_QualityDialog.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
    #endregion //MONOBEHAVIOUR_METHODS


    #region IUserDefinedTargetEventHandler Implementation
    /// <summary>
    /// Called when UserDefinedTargetBuildingBehaviour has been initialized successfully
    /// </summary>
    public void OnInitialized()
    {
        m_ObjectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (m_ObjectTracker != null)
        {
            // Create a new dataset
            m_UDT_DataSet = m_ObjectTracker.CreateDataSet();
            m_ObjectTracker.ActivateDataSet(m_UDT_DataSet);
        }
    }

    /// <summary>
    /// Updates the current frame quality
    /// </summary>
    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {
        //Debug.Log("Frame quality changed: " + frameQuality.ToString());
        m_FrameQuality = frameQuality;
        if (m_FrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_LOW)
        {
            //Debug.Log("Low camera image quality");
        }

        m_FrameQualityMeter.SetQuality(frameQuality);
    }

    /// <summary>
    /// Takes a new trackable source and adds it to the dataset
    /// This gets called automatically as soon as you 'BuildNewTarget with UserDefinedTargetBuildingBehaviour
    /// </summary>
    public void OnNewTrackableSource(TrackableSource trackableSource)
    {
        m_TargetCounter++;

        // Deactivates the dataset first
        m_ObjectTracker.DeactivateDataSet(m_UDT_DataSet);

        // Destroy the oldest target if the dataset is full or the dataset 
        // already contains five user-defined targets.
        if (m_UDT_DataSet.HasReachedTrackableLimit() || m_UDT_DataSet.GetTrackables().Count() >= MAX_TARGETS)
        {
            IEnumerable<Trackable> trackables = m_UDT_DataSet.GetTrackables();
            Trackable oldest = null;
            foreach (Trackable trackable in trackables)
            {
                if (oldest == null || trackable.ID < oldest.ID)
                    oldest = trackable;
            }

            if (oldest != null)
            {
                Debug.Log("Destroying oldest trackable in UDT dataset: " + oldest.Name);
                m_UDT_DataSet.Destroy(oldest, true);
            }
        }

        // Get predefined trackable and instantiate it
        ImageTargetBehaviour imageTargetCopy = Instantiate(ImageTargetTemplate);
        imageTargetCopy.gameObject.name = "UserDefinedTarget-" + m_TargetCounter;

        // Add the duplicated trackable to the data set and activate it
        m_UDT_DataSet.CreateTrackable(trackableSource, imageTargetCopy.gameObject);

        // Activate the dataset again
        m_ObjectTracker.ActivateDataSet(m_UDT_DataSet);

        // Extended Tracking with user defined targets only works with the most recently defined target.
        // If tracking is enabled on previous target, it will not work on newly defined target.
        // Don't need to call this if you don't care about extended tracking.
        StopExtendedTracking();
        m_ObjectTracker.Stop();
        m_ObjectTracker.ResetExtendedTracking();
        m_ObjectTracker.Start();

        // Make sure TargetBuildingBehaviour keeps scanning...
        m_TargetBuildingBehaviour.StartScanning();
    }
    #endregion IUserDefinedTargetEventHandler implementation


    #region PUBLIC_METHODS
    /// <summary>
    /// Instantiates a new user-defined target and is also responsible for dispatching callback to 
    /// IUserDefinedTargetEventHandler::OnNewTrackableSource
    /// </summary>
    public void BuildNewTarget()
    {
        if (m_FrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_MEDIUM ||
            m_FrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH)
        {
            // create the name of the next target.
            // the TrackableName of the original, linked ImageTargetBehaviour is extended with a continuous number to ensure unique names            
            StartCoroutine(DetectImage());

            // generate a new target:

        }
        else
        {
            Debug.Log("Cannot build new target, due to poor camera image quality");
            if (m_QualityDialog)
            {
                StopAllCoroutines();
                m_QualityDialog.GetComponent<CanvasGroup>().alpha = 1;
                StartCoroutine(FadeOutQualityDialog());
            }
        }
    }

    #endregion //PUBLIC_METHODS


    #region PRIVATE_METHODS

    IEnumerator FadeOutQualityDialog()
    {
        yield return new WaitForSeconds(1f);
        CanvasGroup canvasGroup = m_QualityDialog.GetComponent<CanvasGroup>();

        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            f = (float)Math.Round(f, 1);
            Debug.Log("FadeOut: " + f);
            canvasGroup.alpha = (float)Math.Round(f, 1);
            yield return null;
        }
    }

    /// <summary>
    /// This method only demonstrates how to handle extended tracking feature when you have multiple targets in the scene
    /// So, this method could be removed otherwise
    /// </summary>
    void StopExtendedTracking()
    {
        // If Extended Tracking is enabled, we first disable it for all the trackables
        // and then enable it only for the newly created target
        bool extTrackingEnabled = m_TrackableSettings && m_TrackableSettings.IsExtendedTrackingEnabled();
        if (extTrackingEnabled)
        {
            StateManager stateManager = TrackerManager.Instance.GetStateManager();

            // 1. Stop extended tracking on all the trackables
            foreach (var tb in stateManager.GetTrackableBehaviours())
            {
                var itb = tb as ImageTargetBehaviour;
                if (itb != null)
                {
                    itb.ImageTarget.StopExtendedTracking();
                }
            }

            // 2. Start Extended Tracking on the most recently added target
            List<TrackableBehaviour> trackableList = stateManager.GetTrackableBehaviours().ToList();
            ImageTargetBehaviour lastItb = trackableList[LastTargetIndex] as ImageTargetBehaviour;
            if (lastItb != null)
            {
                if (lastItb.ImageTarget.StartExtendedTracking())
                    Debug.Log("Extended Tracking successfully enabled for " + lastItb.name);
            }
        }
    }

    #endregion //PRIVATE_METHODS

    public IEnumerator DetectImage()
    {
        holder = false;
        Camera camera = GameObject.Find("ARCamera").GetComponent<Camera>();
        GameObject.Find("Title").GetComponent<Text>().text = "Scanning..";
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

        //for (int x = 0; x < screenShot.width; x++)
        //{
        //    for (int y = 0; y < screenShot.height; y++)
        //    {                
        //        Color newColor = new Color(0.3F, 0.4F, 0.6F);                
        //        screenShot.SetPixel(x, y, newColor); // Now greyscale
        //    }
        //}
        //screenShot.Apply();
        // reset active camera texture and render texture
        camera.targetTexture = null;
        RenderTexture.active = null;

        fileData = screenShot.EncodeToPNG();
        print("Size: "+fileData.Length);
        headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json; charset=UTF-8");

        string base64 = Convert.ToBase64String(fileData);

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
                    OnAnnotateImageResponses(responses);
                }
                else
                {
                    Debug.Log("Error: " + www.error);
                    GameObject.Find("Title").GetComponent<Text>().text = "System Error!";
                }
            }
        }
    }

    void OnAnnotateImageResponses(AnnotateImageResponses responses)
    {               
        holder = false;
        for (int i = 0; i < responses.responses[0].webDetection.webEntities.Count; i++) // loop thru all responses
        {
            if ((trash.TrashBase.ToUpper().Replace(" ", String.Empty).Contains(responses.responses[0].webDetection.webEntities[i].description.ToUpper().Replace(" ", String.Empty)) ||
               trash.TrashName.ToUpper().Replace(" ", String.Empty).Contains(responses.responses[0].webDetection.webEntities[i].description.ToUpper().Replace(" ", String.Empty))))
            {
                holder = true;
                break;          
            }
        }

        if (holder)
        {
            ScoreScript.AddPoints(10);
            GameObject.Find("Title").GetComponent<Text>().text = "Correct!";           
            string targetName = string.Format("{0}-{1}", ImageTargetTemplate.TrackableName, m_TargetCounter);
            m_TargetBuildingBehaviour.BuildNewTarget(targetName, ImageTargetTemplate.GetSize().x);

        }
        else {

            GameObject.Find("Title").GetComponent<Text>().text = "Incorrect trash!";
            lifeManager.TakeLife();//reduce life
            if (lifeManager.GetCurHealth() == 0)
            {
                BackBtn.enabled = false;
                BuildBtn.enabled = false;
                noLifeDetails.SetActive(true);
                OkayBtn.onClick.AddListener(delegate ()
                {
                    SceneManager.LoadScene("map");
                });
            }
                

        }
        print("END");
    }

    public void ConfigBtn()
    {
        for(int i=0; i< typeBtn.Length; i++)
        {
            typeBtn[i] = GameObject.Find("TypeBtn" + (i + 1));
        }

        int gameLevel = LevelManager.GetInstance().GetLevel();
        if(gameLevel == 1 || gameLevel == 2)
        {
            typeBtn[0].SetActive(true);
            typeBtn[1].SetActive(true);
            typeBtn[2].SetActive(false);
            typeBtn[3].SetActive(false);
        }
        else
        {
            typeBtn[0].SetActive(true);
            typeBtn[1].SetActive(true);
            typeBtn[2].SetActive(true);
            typeBtn[3].SetActive(true);
        }
    }
    

}