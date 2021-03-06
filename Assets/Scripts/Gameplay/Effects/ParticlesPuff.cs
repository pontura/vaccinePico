using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// kzlukos@gmail.com
// Toggles objects visibility and lunches particle system
public class ParticlesPuff : MonoBehaviour {


	//
	[SerializeField]
	private Color emmisionColor = Color.white;
	private ParticleSystem _particles;
	private Renderer _renderer;
	private float _time = 1f;
	private float _emission = 0f;
	private float _emmisionChangeVelocity;
	private Material _material;


	//
	private bool _show = false;
	public bool Show { 
		get { return _show; }
		set 
		{
			if (_particles != null && _show != value)
				_particles.Play ();
			_show = value;
		}
	}

    //
    public void PlayParticles()
    {
        if (_particles != null)
            _particles.Play();
    }

    //
    void Start()
	{
        _emission = 0f;
        _particles = GetComponentInChildren<ParticleSystem> ();
		_renderer = GetComponent<Renderer> ();
        _renderer.enabled = false;
        _material = _renderer.material;
		_material.EnableKeyword("_EMISSION");
		_material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
	}

	//
	void Update()
	{
		_emission = Mathf.SmoothDamp (_emission, Show ? 0f : 1f, ref _emmisionChangeVelocity, _time);
		_material.SetColor("_EmissionColor", emmisionColor * _emission);
		DynamicGI.SetEmissive (_renderer, emmisionColor * _emission);
		_renderer.UpdateGIMaterials ();

		_renderer.enabled = _emission < 0.8f;

	}

	public void Reset()
	{
		if (_particles != null)
			_particles.Clear ();
	}


}
