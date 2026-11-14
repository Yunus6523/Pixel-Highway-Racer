using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SongController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public List<GameObject> songs;
    public GameObject currentSong;
    public int songIndex;
    public int indexMaxRange;

    public bool isInSong;

    void Start()
    {
        isInSong = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInSong == false)
        {
            
            songIndex = Random.Range(0, indexMaxRange);

            currentSong = songs[songIndex];
            currentSong.SetActive(true);
            

            switch (songIndex)
            {
                case 0:
                    StartCoroutine(resetSong(303));
                    GameManager.instance.musicUi.SetActive(true);
                    GameManager.instance.title.SetText("Butterflies & Hurricanes");
                    GameManager.instance.artist.SetText("Muse - Absolution");
                    StartCoroutine(GameManager.instance.turnOffMusicUi());
                    break;
                case 1:
                    StartCoroutine(resetSong(255));
                    GameManager.instance.musicUi.SetActive(true);
                    GameManager.instance.title.SetText("Barracuda");
                    GameManager.instance.artist.SetText("Heart - Little Queen");
                    StartCoroutine(GameManager.instance.turnOffMusicUi());
                    break;
                case 2:
                    StartCoroutine(resetSong(300));
                    GameManager.instance.musicUi.SetActive(true);
                    GameManager.instance.title.SetText("Max Power (Extended Mix)");
                    GameManager.instance.artist.SetText("Dr. Love feat. D.Essex - Super Eurobeat Vol. 105");
                    StartCoroutine(GameManager.instance.turnOffMusicUi());
                    break;
                case 3:
                    StartCoroutine(resetSong(243));
                    GameManager.instance.musicUi.SetActive(true);
                    GameManager.instance.title.SetText("Saw Lightning");
                    GameManager.instance.artist.SetText("Beck - Hyperspace");
                    StartCoroutine(GameManager.instance.turnOffMusicUi());
                    break;
                case 4:
                    StartCoroutine(resetSong(198));
                    GameManager.instance.musicUi.SetActive(true);
                    GameManager.instance.title.SetText("Decadence");
                    GameManager.instance.artist.SetText("Disturbed - Ten Thousand Fists");
                    StartCoroutine(GameManager.instance.turnOffMusicUi());
                    break;
                case 5:
                    StartCoroutine(resetSong(321.6f));
                    GameManager.instance.musicUi.SetActive(true);
                    GameManager.instance.title.SetText("Get Low");
                    GameManager.instance.artist.SetText("Lil Jon & The East Side Boyz feat. Ying Yang Twins - Kings of Crunk");
                    StartCoroutine(GameManager.instance.turnOffMusicUi());
                    break;
                case 6:
                    StartCoroutine(resetSong(243));
                    GameManager.instance.musicUi.SetActive(true);
                    GameManager.instance.title.SetText("Eat Your Young");
                    GameManager.instance.artist.SetText("Hozier - Unreal Unearth");
                    StartCoroutine(GameManager.instance.turnOffMusicUi());
                    break;
                case 7:
                    StartCoroutine(resetSong(192));
                    GameManager.instance.musicUi.SetActive(true);
                    GameManager.instance.title.SetText("Lift Me Up");
                    GameManager.instance.artist.SetText("Moby - Hotel(Moby)");
                    StartCoroutine(GameManager.instance.turnOffMusicUi());
                    break;
                case 8:
                    StartCoroutine(resetSong(253.2f));
                    GameManager.instance.musicUi.SetActive(true);
                    GameManager.instance.title.SetText("Megalomaniac (Single Mix)");
                    GameManager.instance.artist.SetText("KMFDM - EXTRA Vol. 3");
                    StartCoroutine(GameManager.instance.turnOffMusicUi());
                    break;
                case 9:
                    StartCoroutine(resetSong(205.8f));
                    GameManager.instance.musicUi.SetActive(true);
                    GameManager.instance.title.SetText("MARK CHAPMAN");
                    GameManager.instance.artist.SetText("Maneskin - RUSH!");
                    StartCoroutine(GameManager.instance.turnOffMusicUi());
                    break;
            }
            isInSong = true;
        }
    }

    IEnumerator resetSong(float time)
    {
        yield return new WaitForSeconds(time);
        currentSong.SetActive(false);
        isInSong = false;
    }
}
