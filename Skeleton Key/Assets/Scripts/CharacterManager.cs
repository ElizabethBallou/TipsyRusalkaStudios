using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] characters;

    public List<GameObject> actorsList = new List<GameObject>();
    List<Actor> activeActors = new List<Actor>();
    [SerializeField] private Vector3 leftActorPosition, rightActorPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            GameObject newActor = Instantiate(characters[i]);
            newActor.SetActive(false);
            newActor.name = characters[i].name;
            actorsList.Add(newActor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceActor(string leftActorName, string rightActorName)
    {
        foreach (GameObject actorObject in actorsList)
        {
            if (actorObject.name == leftActorName)
            {
                actorObject.SetActive(true);
                actorObject.GetComponent<Actor>().ID = 0;
                activeActors.Add(actorObject.GetComponent<Actor>());
                actorObject.transform.position = leftActorPosition;
            }
            else if (actorObject.name == rightActorName)
            {
                actorObject.SetActive(true);
                actorObject.GetComponent<Actor>().ID = 1;
                activeActors.Add(actorObject.GetComponent<Actor>());
                actorObject.transform.position = rightActorPosition;
            }
        }
    }

    public void ChangeActorEmotion(string emotion, int ID)
    {
        foreach (Actor actor in activeActors)
        {
            if (actor.gameObject.activeInHierarchy)
            {
                if (actor.ID == ID)
                {
                    actor.ChangeState(emotion);
                }
            }
        }
    }
}
