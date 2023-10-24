using UnityEngine;
using System.Collections;

public class SkyLightProp : MonoBehaviour
{
    public float minIntensity = 0f;     // ���ǿ��
    public float maxIntensity = 1.5f;     // ���ǿ��
    public float minWaitTime = 0.5f;      // ��̵ȴ�ʱ��
    public float maxWaitTime = 5.0f;      // ��ȴ�ʱ��
    public float minWaitTime2 = 3f;      // ��̵ȴ�ʱ��
    public float maxWaitTime2 = 10.0f;      // ��ȴ�ʱ��

    private Light myLight;
    private float targetIntensity;

    private void Start()
    {
        myLight = GetComponent<Light>();
        StartCoroutine(RandomizeIntensity());
    }

    private IEnumerator RandomizeIntensity()
    {
        while (true)
        {
            // ����ȴ�ʱ��
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            float waitTime2 = Random.Range(minWaitTime2, maxWaitTime2);
            yield return new WaitForSeconds(waitTime);

            // ���Ŀ��ǿ��
            targetIntensity = Random.Range(minIntensity, maxIntensity);

            // ƽ���ع��ɵ�Ŀ��ǿ��
            float elapsedTime = 0f;
            while (elapsedTime < waitTime)
            {
                myLight.intensity = Mathf.Lerp(myLight.intensity, targetIntensity, elapsedTime / waitTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            elapsedTime = 0f;
            yield return new WaitForSeconds(waitTime2);

            while (elapsedTime < waitTime2)
            {
                myLight.intensity = Mathf.Lerp(myLight.intensity, minIntensity, elapsedTime / waitTime2);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // �ȴ���һ�����ǿ�ȱ仯
            yield return null;
        }
    }
}

