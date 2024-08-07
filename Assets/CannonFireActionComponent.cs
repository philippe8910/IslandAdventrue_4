using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CannonFireActionComponent : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject effectPrefab;

    [SerializeField] private Transform mazzPos;
    [SerializeField] private Transform effectPos;

    [SerializeField] private LineRenderer moveLine;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float reloadTime;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private bool reloading;

    [ContextMenu("FireAction")]
    public async void FireAction()
    {
        if(reloading)
        {
            return;
        }

        var bullet = Instantiate(bulletPrefab , mazzPos.position , Quaternion.identity);
        var effect = Instantiate(effectPrefab , effectPos.position , Quaternion.identity);

        moveLine.gameObject.SetActive(false);

        reloading = true;

        for(int i = 0 ; i < moveLine.positionCount ; i++)
        {
            bullet.transform.DOMove(moveLine.GetPosition(i) + new Vector3(0, 0.1f,0) , bulletSpeed);
            await Task.Delay(30);
        }

        if(bullet != null)
            Destroy(bullet);

        Destroy(effect);

        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        moveLine.gameObject.SetActive(true);
        audioSource.Play();
        reloading = false;
    }
}
