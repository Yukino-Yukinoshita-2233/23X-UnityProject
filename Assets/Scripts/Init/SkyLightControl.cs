using UnityEngine;
using System.Collections;

public class SkyLightControl : MonoBehaviour
{
    public float minIntensity = 0f;     // 最低强度
    public float maxIntensity = 1.5f;     // 最高强度
    public float minWaitTime = 0.5f;      // 最短等待时间
    public float maxWaitTime = 5.0f;      // 最长等待时间
    public float minWaitTime2 = 3f;      // 最短等待时间
    public float maxWaitTime2 = 10.0f;      // 最长等待时间

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
            // 随机等待时间
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            float waitTime2 = Random.Range(minWaitTime2, maxWaitTime2);
            yield return new WaitForSeconds(waitTime);

            // 随机目标强度
            targetIntensity = Random.Range(minIntensity, maxIntensity);

            // 平滑地过渡到目标强度
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

            // 等待下一个随机强度变化
            yield return null;
        }
    }
}

