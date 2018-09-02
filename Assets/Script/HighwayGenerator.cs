using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighwayGenerator : MonoBehaviour {
    //Префаб для точки поворота
    [SerializeField]
    private GameObject wayPoint;

    //Родительский элемент путя
    [SerializeField]
    private Transform folderHWI;
    //Родительский элемент путя
    [SerializeField]
    private Transform folderHWO;
    //Длина трассы - количество точек поворота (Минимум две - начало и конец)
    [SerializeField]
    private int lengthWay = 2;
    //Список точек поворота
    private List<GameObject> highWay = new List<GameObject>();
    //Минимальная длинна между точками
    [SerializeField]
    private float lGapMin;
    //Максимальная длинна между точками
    [SerializeField]
    private float lGapMax;
    //Минимальный угол поворота
    [SerializeField]
    private float constrainAngleMin = 20;
    //Максимальный угол поворота
    [SerializeField]
    private float constrainAngleMax = 60;
    //Радиус wayPoint
    [SerializeField]
    private float radius = 10;

    private void Awake()
    {
        lengthWay = RunnerInfo.lengthWay;
    }

    // Use this for initialization
    void Start () {
        if (lengthWay < 2) lengthWay = 2;
        wayPointGenerator();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Функция генерации точек поворота
    private void wayPointGenerator()
    {
        //Создание стартовой точки
        highWay.Add(Instantiate(wayPoint, new Vector3(0,0,0), Quaternion.identity, folderHWI));
        highWay[0].GetComponent<wayPointParam>().posWPoint = new Vector3(0, 0, 0);
        highWay[0].SetActive(false);
        //Создание второй точки
        highWay.Add(Instantiate(wayPoint, new Vector3(0, 0, lGapMin*2-radius), Quaternion.identity, folderHWI));
        highWay[1].GetComponent<wayPointParam>().posWPoint = new Vector3(0, 0, lGapMin * 2);
        GetComponent<GeneratorAsteroid>().generate(lGapMin * 2 -  radius, radius + lGapMin*0.7f, 0, highWay[0].transform.position, highWay[0].transform.rotation);
        highWay[1].GetComponent<wayPointParam>().index = 1;
        highWay[1].tag = "Inner";
        GameObject clone2 = Instantiate(highWay[1], new Vector3(0, 0, (lGapMin * 2 - radius*2)), Quaternion.identity, folderHWI);
        clone2.tag = "Warning";
        for (int i = 2; i < lengthWay; i++)
        {
            //Получение угла между точками
            float angle = Random.Range(constrainAngleMin, constrainAngleMax+1);
            bool dir = Random.Range(0, 2) == 1;
            Debug.Log("a " + dir);
            angle = dir ? angle : -angle;
            angle *= Mathf.Deg2Rad;

            //Стартовое значение положения новой точки
            Vector3 posNewPoint = highWay[i - 1].GetComponent<wayPointParam>().posWPoint;
            //Нормализованное положенние предыдущей точки
            Vector3 posPrevPoint = (posNewPoint - highWay[i - 2].GetComponent<wayPointParam>().posWPoint).normalized;
            //Рассчет смещения новой точки относительно предыдущей
            float x = posPrevPoint.x * Mathf.Cos(angle) - posPrevPoint.z * Mathf.Sin(angle);
            float z = posPrevPoint.x * Mathf.Sin(angle) + posPrevPoint.z * Mathf.Cos(angle);
            posPrevPoint = new Vector3(x, 0, z);
            float l = Random.Range(lGapMin, lGapMax);
            Vector3 ppp2 = posPrevPoint * radius;
            Vector3 pnp2 = posNewPoint;
           
            Vector3 pwp = new Vector3(posNewPoint.x + posPrevPoint.x*l, 0, posNewPoint.z + posPrevPoint.z*l);
            Vector3 forWarning = posPrevPoint * (l -radius * 2);
            Vector3 forWarning2 = posNewPoint;
            posPrevPoint *= l-radius;
            //Конечное значение положения новой точки
            posNewPoint = new Vector3(posNewPoint.x + posPrevPoint.x, 0, posNewPoint.z + posPrevPoint.z);
            //Создание новой точки
            highWay.Add(Instantiate(wayPoint, posNewPoint, Quaternion.identity, folderHWI));

            highWay[i].tag = "Inner";
            // highWay.Add(Instantiate(wayPoint, posNewPoint, Quaternion.identity, folderHW));
            //
            highWay[i].transform.Rotate(Vector3.up, Vector3.SignedAngle(Vector3.forward, posPrevPoint, Vector3.up));
            pnp2 = new Vector3(pnp2.x + ppp2.x, 0, pnp2.z + ppp2.z);
            GameObject clone = Instantiate(highWay[i], pnp2, highWay[i].transform.rotation, folderHWI);
            clone.tag = "Outer";
            if(i != lengthWay - 1)
            {
                clone2 = Instantiate(highWay[i], new Vector3(forWarning2.x + forWarning.x, 0, forWarning2.z + forWarning.z), highWay[i].transform.rotation, folderHWO);
                clone2.tag = "Warning";
            }
            highWay[i].GetComponent<wayPointParam>().posWPoint = pwp;
            highWay[i].GetComponent<wayPointParam>().index = i;
            clone.GetComponent<wayPointParam>().index = i-1;
            //clone.transform.Rotate(highWay[i].transform.rotation.eulerAngles);
            highWay[i-1].GetComponent<wayPointParam>().calcRotatetPoint(highWay[i-1].transform, angle, radius, dir);
            GetComponent<GeneratorAsteroid>().generate(l - radius * 2, radius, 0, clone.transform.position, clone.transform.rotation);

        }
        highWay[lengthWay - 1].tag = "Finish";
    }
   
}
