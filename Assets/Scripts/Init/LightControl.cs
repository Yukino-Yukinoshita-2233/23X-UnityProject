using UnityEngine;
using System.Collections;

public class LightControl : MonoBehaviour
{
    public float minSpotAngle = 0.5f;  // 最低强度
    public float maxSpotAngle = 100f;  // 最高强度
    public float minWaitTime = 0.5f;      // 最短等待时间
    public float maxWaitTime = 2.0f;      // 最长等待时间
    public float speed = 0.1f;         // 呼吸速度

    private Light myLight;             // 或者用于控制的材质

    private float targetSpotAngle;

    private void Start()
    {
        myLight = GetComponent<Light>();  // 如果是灯光，获取灯光组件
        targetSpotAngle = maxSpotAngle;
        StartCoroutine(RandomizeIntensity());
    }

    private IEnumerator RandomizeIntensity()
    {
        while (true)
        {
            // 随机等待时间
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            // 随机目标强度
            targetSpotAngle = Random.Range(minSpotAngle, maxSpotAngle);

            // 平滑地过渡到目标强度
            float elapsedTime = 0f;
            while (elapsedTime < waitTime)
            {
                myLight.innerSpotAngle = Mathf.Lerp(myLight.innerSpotAngle, targetSpotAngle, elapsedTime * speed / waitTime);
                myLight.spotAngle = Mathf.Lerp(myLight.spotAngle, targetSpotAngle, elapsedTime * speed / waitTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 等待下一个随机强度变化
            yield return null;
        }
    }
}
