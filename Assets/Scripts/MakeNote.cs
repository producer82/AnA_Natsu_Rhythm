/*********************************
 * Parser (GameObject)
 * ㄴ MakeNote.cs
 * 
 * 비트맵 파싱 및 노트 생성에 관한 작업을 처리함
 * ******************************/

using OsuParsers.Beatmaps;
using OsuParsers.Beatmaps.Objects.Mania;
using OsuParsers.Decoders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

/// 메타데이터 섹션 구조체
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

/// 제네럴 섹션 구조체
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

    // Metadata 구조체 선언
    public Metadata metadata;

    // General 구조체 선언
    public General general;

    // 기타
    float velocity = 0f; // 슬라이더의 속도
    bool songFlag = true; // 노래 재생 플래그

    /// 게임 시작과 동시에 호출됨
    /// 노트 재생 전에 필요한 작업을 처리함
    void Start(int songNumber)
    {
        //파서 경로 설정
        beatmap = BeatmapDecoder.Decode(Application.dataPath + "/StreamingAssets/LoveDramatic.osu");

        //4레인 마니아 비트맵인지 확인
        CheckBeatmap();

        //기본 비트맵 데이터 얻기
        velocity = (float)beatmap.DifficultySection.SliderMultiplier;
        GetGeneral();
        GetMetadata();

        //노트 생성 및 노래 재생
        CreateNote();
    }

    /// 한 프레임마다 호출됨
    /// 노트를 스크롤 함
    void FixedUpdate()
    {
        if(songFlag)
        {
            GameObject.Find("Music").GetComponent<AudioSource>().Play();
            songFlag = false;
        }
        // Time.deltaTime을 곱해주어 프레임 별 스크롤 속도가 달라지는 일을 막음
        transform.position -= new Vector3(0, 100 * velocity * Time.deltaTime, 0);
    }

    /// 노트를 생성함
    void CreateNote()
    {
        // 노트의 게임 오브젝트 원형
        GameObject Note = GameObject.Find("Note");

        // 레인별로 노트를 출력
        for(int i = 0; i < beatmap.HitObjects.Count; i++)
        {
            if (beatmap.HitObjects[i].Position.X == 64)
            {
                Instantiate(Note, new Vector3(FIRST_RAIN, (beatmap.HitObjects[i].StartTime + general.audioLeadIn) / (10 / velocity), 0), 
                    Quaternion.identity, transform);
            }
            else if (beatmap.HitObjects[i].Position.X == 192)
            {
                Instantiate(Note, new Vector3(SECOND_RAIN, (beatmap.HitObjects[i].StartTime + general.audioLeadIn) / (10 / velocity), 0), 
                    Quaternion.identity, transform);
            }
            else if (beatmap.HitObjects[i].Position.X == 320)
            {
                Instantiate(Note, new Vector3(THIRD_RAIN, (beatmap.HitObjects[i].StartTime + general.audioLeadIn) / (10 / velocity), 0), 
                    Quaternion.identity, transform);
            }
            else if (beatmap.HitObjects[i].Position.X == 448)
            {
                Instantiate(Note, new Vector3(FOURTH_RAIN, (beatmap.HitObjects[i].StartTime + general.audioLeadIn) / (10 / velocity), 0), 
                    Quaternion.identity, transform);
            }
            else
            {
                Debug.Log("지정되지 않은 위치입니다. : " + beatmap.HitObjects[i].Position.X);
            }
        }
    }

    /// 비트맵의 General 섹션 데이터를 받아옴
    void GetGeneral()
    {
        general.audioLeadIn = beatmap.GeneralSection.AudioLeadIn;
        general.previewTime = beatmap.GeneralSection.PreviewTime;
        general.length = beatmap.GeneralSection.Length;
    }

    /// 비트맵의 Metadata 섹션 데이터를 받아옴
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

    /// 비트맵의 기본 요건을 확인함
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
