using UnityEngine;
using System.Collections;

public class LightControl : MonoBehaviour
{
    public float minSpotAngle = 0.5f;  // ���ǿ��
    public float maxSpotAngle = 100f;  // ���ǿ��
    public float minWaitTime = 0.5f;      // ��̵ȴ�ʱ��
    public float maxWaitTime = 2.0f;      // ��ȴ�ʱ��
    public float speed = 0.1f;         // �����ٶ�

    private Light myLight;             // �������ڿ��ƵĲ���

    private float targetSpotAngle;

    private void Start()
    {
        myLight = GetComponent<Light>();  // ����ǵƹ⣬��ȡ�ƹ����
        targetSpotAngle = maxSpotAngle;
        StartCoroutine(RandomizeIntensity());
    }

    private IEnumerator RandomizeIntensity()
    {
        while (true)
        {
            // ����ȴ�ʱ��
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            // ���Ŀ��ǿ��
            targetSpotAngle = Random.Range(minSpotAngle, maxSpotAngle);

            // ƽ���ع��ɵ�Ŀ��ǿ��
            float elapsedTime = 0f;
            while (elapsedTime < waitTime)
            {
                myLight.innerSpotAngle = Mathf.Lerp(myLight.innerSpotAngle, targetSpotAngle, elapsedTime * speed / waitTime);
                myLight.spotAngle = Mathf.Lerp(myLight.spotAngle, targetSpotAngle, elapsedTime * speed / waitTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // �ȴ���һ�����ǿ�ȱ仯
            yield return null;
        }
    }
}
