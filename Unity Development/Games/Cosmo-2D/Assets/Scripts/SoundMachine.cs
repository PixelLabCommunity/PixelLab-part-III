using UnityEngine;

public class SoundMachine : MonoBehaviour
{
    [SerializeField] private AudioSource collectSoundEffect;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource finishSoundEffect;
    [SerializeField] private AudioSource jumpSoundEffect;

    private void Update()
    {
        PlaySoundEffects();
    }

    private void PlaySoundEffects()
    {
        if (PlayerMovement.Instance.jumping)
            PlaySound(jumpSoundEffect, ref PlayerMovement.Instance.jumping, "PlayerJump Sound Effect");

        if (PlayerLife.Instance.death)
            PlaySound(deathSoundEffect, ref PlayerLife.Instance.death, "PlayerDeath Sound Effect");

        if (ItemCollector.Instance.collected)
            PlaySound(collectSoundEffect, ref ItemCollector.Instance.collected, "PlayerCollect Sound Effect");

        if (Finish.Instance.finished)
            PlaySound(finishSoundEffect, ref Finish.Instance.finished, "Finish Sound Effect");
    }

    private static void PlaySound(AudioSource soundEffect, ref bool condition, string warningMessage)
    {
        switch (soundEffect)
        {
            case null:
                Debug.LogWarning($"Please Set {warningMessage} into the Sound Machine!");
                break;
            default:
                soundEffect.Play();
                condition = false;
                break;
        }
    }
}