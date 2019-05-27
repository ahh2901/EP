using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GoogleApi : MonoBehaviour {

    public RawImage img;

    string url;

    public double lat;
    public double lon;

    LocationInfo li;

    private bool onDragging = false;

    public double onDragLat;
    public double onDragLon;

    public double onDragX;
    public double onDragY;


    public int zoom = 14;
    public int mapWidth = 640;
    public int mapHeight= 640;

    public enum mapType {roadmap, satelite, hybrid, terrain}
    public mapType mapSelected;
    public int scale;

    private WWW www;

    IEnumerator Map()
    {
        // API Key : AIzaSyDDyFR0Z-IndO-EmkzlIiFGxBawKmgxOEA 

        while (true)
        {
            url = "https://maps.googleapis.com/maps/api/staticmap?center=" + //구글 API?
            lat + "," + lon + "&zoom=" + zoom + "&size=" + //위도, 경도, 줌
            mapHeight + "x" + mapWidth + "&Scale=" + scale + "&maptype=" + mapSelected + //맵의 크기, 맵 모드 세팅
            "&markers=color:blue%7Clabel:S%7C37.283065,127.046462&markers=color:red%7Clabel:G%7C37.283065,127.056562" + //마커를 세팅하는 코드, 마커 위치, 색상 등을 표시
            "&key=AIzaSyDDyFR0Z-IndO-EmkzlIiFGxBawKmgxOEA"; // 구글 API 키값

            www = new WWW(url);

            yield return www;

            //lat +=  0.001f; // 테스트코드
            img.texture = www.texture;
            img.SetNativeSize();

            float a = Time.deltaTime;
            Debug.Log("lat: " + lat + "\nlon : " + lon);
            //Debug.Log("a: "+a); // deltaTime 이 0.01초 마다 된다고 출력되는데 실제 맵 갱신은 좀 더 느림(0.2~3초 마다 갱신되는 느낌?)

            yield return new WaitForSeconds(a);

        }
    }

    // Use this for initialization
    void Start () {
        img = gameObject.GetComponent<RawImage>();
        StartCoroutine (Map());
        

	}
	
	// Update is called once per frame
	void Update () {
        //lat += Time.deltaTime * 0.5;
        Canvas.ForceUpdateCanvases();

        if(onDragging == true)
        {
            lat = onDragLat + (onDragY - Input.mousePosition.y) * 0.00001;
            lon = onDragLon + (onDragX - Input.mousePosition.x) * 0.00001;
            //드래그 중인 동안 클릭시작위치에 + 마우스 이동으로 변경된 위지 추가
        }

        if (Input.GetMouseButtonDown(0))
        {
            onDragging = true;

            onDragLat = lat;
            onDragLon = lon;
            // 드래그 시작 위치 입력

            onDragX = Input.mousePosition.x;
            onDragY = Input.mousePosition.y;
            // 드래그 시작 좌표 입력
        }

        if (Input.GetMouseButtonUp(0))
        {
            onDragging = false;
            // 드래그 그만두는거 처리
        }
    }

    void OnMouseDrag()
    {
        Debug.Log("드래그 중2");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //드래그 시작
        Debug.Log("드래그 시작!");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //드래그 중
        Debug.Log("드래그 중");

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //드래그 끝
        Debug.Log("드래그 끝!");

    }
}
