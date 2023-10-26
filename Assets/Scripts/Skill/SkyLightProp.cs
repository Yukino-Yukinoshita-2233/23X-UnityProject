using UnityEngine;
using System.Collections;

public class SkyLightProp : MonoBehaviour
{
    public float maxIntensity = 1f;     // ���ǿ��

    private Light myLight;
    private float targetIntensity;

    private void Start()
    {
        myLight = GetComponent<Light>();
    }

    private void Update()
    {
        StartCoroutine(RandomizeIntensity());

    }
    private IEnumerator RandomizeIntensity()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q");
            // ƽ���ع��ɵ�Ŀ��ǿ��
            float elapsedTime = 0f;
            while (elapsedTime < 1)
            {
                myLight.intensity = Mathf.Lerp(myLight.intensity, maxIntensity, elapsedTime / 3);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            elapsedTime = 0f;
            yield return new WaitForSeconds(1);

            while (elapsedTime < 1)
            {
                myLight.intensity = Mathf.Lerp(myLight.intensity, 0, elapsedTime / 3);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

        }
    }
}

