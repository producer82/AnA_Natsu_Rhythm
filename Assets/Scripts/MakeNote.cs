using OsuParsers.Beatmaps;
using OsuParsers.Beatmaps.Objects.Mania;
using OsuParsers.Decoders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public struct Metadata
{
    public string title;
    public string artist;
    public string creator;
    public string version;
    public string source;
    public int beatmapID;
    public int beatampSetID;
};

public struct General
{
    public int audioLeadIn;
    public int previewTime;
    public int length;
};

public class MakeNote : MonoBehaviour
{
    // 레인의 좌표에 관한 상수 정의
    const int FIRST_RAIN = -15;
    const int SECOND_RAIN = -5;
    const int THIRD_RAIN = 5;
    const int FOURTH_RAIN = 15;

    // 비트맵 파서 선언
    Beatmap beatmap;

    // Metadata 선언
    public Metadata metadata;

    // General 선언
    public General general;

    // 기타
    public GameObject Note;    // 노트의 원형
    float velocity = 0f; // 슬라이더의 속도

    void Start()
    {
        beatmap = BeatmapDecoder.Decode("./Assets/Beatmaps/LoveDramatic.osu");

        velocity = (float)beatmap.DifficultySection.SliderMultiplier;

        CheckBeatmap();
        GetGeneral();
        GetMetadata();
        CreateNote();

        GameObject.Find("Music").GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position -= new Vector3(0, 100 * velocity * Time.deltaTime, 0);
    }

    void CreateNote()
    {
        for(int i = 0; i < beatmap.HitObjects.Count; i++)
        {
            if (beatmap.HitObjects[i].Position.X == 64)
            {
                Instantiate(Note, new Vector3(FIRST_RAIN, (beatmap.HitObjects[i].StartTime - 100 + general.audioLeadIn) / (10 / velocity), 0), 
                    Quaternion.identity, transform);
            }
            else if (beatmap.HitObjects[i].Position.X == 192)
            {
                Instantiate(Note, new Vector3(SECOND_RAIN, (beatmap.HitObjects[i].StartTime - 100 + general.audioLeadIn) / (10 / velocity), 0), 
                    Quaternion.identity, transform);
            }
            else if (beatmap.HitObjects[i].Position.X == 320)
            {
                Instantiate(Note, new Vector3(THIRD_RAIN, (beatmap.HitObjects[i].StartTime - 100 + general.audioLeadIn) / (10 / velocity), 0), 
                    Quaternion.identity, transform);
            }
            else if (beatmap.HitObjects[i].Position.X == 448)
            {
                Instantiate(Note, new Vector3(FOURTH_RAIN, (beatmap.HitObjects[i].StartTime - 100 + general.audioLeadIn) / (10 / velocity), 0), 
                    Quaternion.identity, transform);
            }
            else
            {
                Debug.Log("지정되지 않은 위치입니다. : " + beatmap.HitObjects[i].Position.X);
            }
        }
    }

    void GetGeneral()
    {
        general.audioLeadIn = beatmap.GeneralSection.AudioLeadIn;
        general.previewTime = beatmap.GeneralSection.PreviewTime;
        general.length = beatmap.GeneralSection.Length;
    }

    void GetMetadata()
    {
        metadata.title = beatmap.MetadataSection.Title;
        metadata.artist = beatmap.MetadataSection.Artist;
        metadata.creator = beatmap.MetadataSection.Creator;
        metadata.version = beatmap.MetadataSection.Version;
        metadata.source = beatmap.MetadataSection.Source;
        metadata.beatmapID = beatmap.MetadataSection.BeatmapID;
        metadata.beatampSetID = beatmap.MetadataSection.BeatmapSetID;
    }

    void CheckBeatmap()
    {
        if (beatmap.GeneralSection.ModeId != 3)
        {
            Debug.Log("osu!mania 비트맵이 아님");
        }
        else if (beatmap.DifficultySection.CircleSize != 4)
        {
            Debug.Log("레인의 갯수가 4개가 아님");
        }
    }
}
