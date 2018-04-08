template <typename T> void RegisterClass();
template <typename T> void RegisterStrippedType(int, const char*, const char*);

void InvokeRegisterStaticallyLinkedModuleClasses()
{
	// Do nothing (we're in stripping mode)
}

void RegisterStaticallyLinkedModulesGranular()
{
	void RegisterModule_Animation();
	RegisterModule_Animation();

	void RegisterModule_Audio();
	RegisterModule_Audio();

	void RegisterModule_CloudWebServices();
	RegisterModule_CloudWebServices();

	void RegisterModule_ParticleSystem();
	RegisterModule_ParticleSystem();

	void RegisterModule_Physics();
	RegisterModule_Physics();

	void RegisterModule_Physics2D();
	RegisterModule_Physics2D();

	void RegisterModule_TextRendering();
	RegisterModule_TextRendering();

	void RegisterModule_UI();
	RegisterModule_UI();

	void RegisterModule_UnityConnect();
	RegisterModule_UnityConnect();

	void RegisterModule_IMGUI();
	RegisterModule_IMGUI();

	void RegisterModule_ImageConversion();
	RegisterModule_ImageConversion();

	void RegisterModule_JSONSerialize();
	RegisterModule_JSONSerialize();

	void RegisterModule_UnityWebRequest();
	RegisterModule_UnityWebRequest();

	void RegisterModule_GameCenter();
	RegisterModule_GameCenter();

	void RegisterModule_Core();
	RegisterModule_Core();

	void RegisterModule_SharedInternals();
	RegisterModule_SharedInternals();

	void RegisterModule_Wind();
	RegisterModule_Wind();

}

class EditorExtension; template <> void RegisterClass<EditorExtension>();
namespace Unity { class Component; } template <> void RegisterClass<Unity::Component>();
class Behaviour; template <> void RegisterClass<Behaviour>();
class Animation; 
class Animator; template <> void RegisterClass<Animator>();
class AudioBehaviour; template <> void RegisterClass<AudioBehaviour>();
class AudioListener; template <> void RegisterClass<AudioListener>();
class AudioSource; template <> void RegisterClass<AudioSource>();
class AudioFilter; 
class AudioChorusFilter; 
class AudioDistortionFilter; 
class AudioEchoFilter; 
class AudioHighPassFilter; 
class AudioLowPassFilter; 
class AudioReverbFilter; 
class AudioReverbZone; 
class Camera; template <> void RegisterClass<Camera>();
namespace UI { class Canvas; } template <> void RegisterClass<UI::Canvas>();
namespace UI { class CanvasGroup; } template <> void RegisterClass<UI::CanvasGroup>();
namespace Unity { class Cloth; } 
class Collider2D; template <> void RegisterClass<Collider2D>();
class BoxCollider2D; template <> void RegisterClass<BoxCollider2D>();
class CapsuleCollider2D; 
class CircleCollider2D; 
class CompositeCollider2D; 
class EdgeCollider2D; 
class PolygonCollider2D; 
class TilemapCollider2D; 
class ConstantForce; 
class Effector2D; 
class AreaEffector2D; 
class BuoyancyEffector2D; 
class PlatformEffector2D; 
class PointEffector2D; 
class SurfaceEffector2D; 
class FlareLayer; template <> void RegisterClass<FlareLayer>();
class GUIElement; template <> void RegisterClass<GUIElement>();
namespace TextRenderingPrivate { class GUIText; } 
class GUITexture; 
class GUILayer; template <> void RegisterClass<GUILayer>();
class GridLayout; 
class Grid; 
class Tilemap; 
class Halo; 
class HaloLayer; 
class Joint2D; 
class AnchoredJoint2D; 
class DistanceJoint2D; 
class FixedJoint2D; 
class FrictionJoint2D; 
class HingeJoint2D; 
class SliderJoint2D; 
class SpringJoint2D; 
class WheelJoint2D; 
class RelativeJoint2D; 
class TargetJoint2D; 
class LensFlare; 
class Light; template <> void RegisterClass<Light>();
class LightProbeGroup; 
class LightProbeProxyVolume; 
class MonoBehaviour; template <> void RegisterClass<MonoBehaviour>();
class NavMeshAgent; 
class NavMeshObstacle; 
class NetworkView; 
class OffMeshLink; 
class PhysicsUpdateBehaviour2D; 
class ConstantForce2D; 
class PlayableDirector; 
class Projector; 
class ReflectionProbe; 
class Skybox; 
class SortingGroup; 
class Terrain; 
class VideoPlayer; 
class WindZone; 
namespace UI { class CanvasRenderer; } template <> void RegisterClass<UI::CanvasRenderer>();
class Collider; template <> void RegisterClass<Collider>();
class BoxCollider; template <> void RegisterClass<BoxCollider>();
class CapsuleCollider; template <> void RegisterClass<CapsuleCollider>();
class CharacterController; 
class MeshCollider; template <> void RegisterClass<MeshCollider>();
class SphereCollider; template <> void RegisterClass<SphereCollider>();
class TerrainCollider; 
class WheelCollider; 
namespace Unity { class Joint; } 
namespace Unity { class CharacterJoint; } 
namespace Unity { class ConfigurableJoint; } 
namespace Unity { class FixedJoint; } 
namespace Unity { class HingeJoint; } 
namespace Unity { class SpringJoint; } 
class LODGroup; 
class MeshFilter; template <> void RegisterClass<MeshFilter>();
class OcclusionArea; 
class OcclusionPortal; 
class ParticleAnimator; 
class ParticleEmitter; 
class EllipsoidParticleEmitter; 
class MeshParticleEmitter; 
class ParticleSystem; template <> void RegisterClass<ParticleSystem>();
class Renderer; template <> void RegisterClass<Renderer>();
class BillboardRenderer; 
class LineRenderer; 
class MeshRenderer; template <> void RegisterClass<MeshRenderer>();
class ParticleRenderer; 
class ParticleSystemRenderer; template <> void RegisterClass<ParticleSystemRenderer>();
class SkinnedMeshRenderer; template <> void RegisterClass<SkinnedMeshRenderer>();
class SpriteMask; 
class SpriteRenderer; template <> void RegisterClass<SpriteRenderer>();
class TilemapRenderer; 
class TrailRenderer; template <> void RegisterClass<TrailRenderer>();
class Rigidbody; template <> void RegisterClass<Rigidbody>();
class Rigidbody2D; 
namespace TextRenderingPrivate { class TextMesh; } 
class Transform; template <> void RegisterClass<Transform>();
namespace UI { class RectTransform; } template <> void RegisterClass<UI::RectTransform>();
class Tree; 
class WorldAnchor; 
class WorldParticleCollider; 
class GameObject; template <> void RegisterClass<GameObject>();
class NamedObject; template <> void RegisterClass<NamedObject>();
class AssetBundle; 
class AssetBundleManifest; 
class ScriptedImporter; 
class AudioMixer; 
class AudioMixerController; 
class AudioMixerGroup; 
class AudioMixerGroupController; 
class AudioMixerSnapshot; 
class AudioMixerSnapshotController; 
class Avatar; 
class AvatarMask; 
class BillboardAsset; 
class ComputeShader; 
class Flare; 
namespace TextRendering { class Font; } template <> void RegisterClass<TextRendering::Font>();
class GameObjectRecorder; 
class LightProbes; template <> void RegisterClass<LightProbes>();
class Material; template <> void RegisterClass<Material>();
class ProceduralMaterial; 
class Mesh; template <> void RegisterClass<Mesh>();
class Motion; template <> void RegisterClass<Motion>();
class AnimationClip; template <> void RegisterClass<AnimationClip>();
class PreviewAnimationClip; 
class NavMeshData; 
class OcclusionCullingData; 
class PhysicMaterial; 
class PhysicsMaterial2D; 
class PreloadData; template <> void RegisterClass<PreloadData>();
class RuntimeAnimatorController; template <> void RegisterClass<RuntimeAnimatorController>();
class AnimatorController; template <> void RegisterClass<AnimatorController>();
class AnimatorOverrideController; 
class SampleClip; template <> void RegisterClass<SampleClip>();
class AudioClip; template <> void RegisterClass<AudioClip>();
class Shader; template <> void RegisterClass<Shader>();
class ShaderVariantCollection; 
class SpeedTreeWindAsset; 
class Sprite; template <> void RegisterClass<Sprite>();
class SpriteAtlas; 
class SubstanceArchive; 
class TerrainData; 
class TextAsset; template <> void RegisterClass<TextAsset>();
class CGProgram; 
class MonoScript; template <> void RegisterClass<MonoScript>();
class Texture; template <> void RegisterClass<Texture>();
class BaseVideoTexture; 
class MovieTexture; 
class WebCamTexture; 
class CubemapArray; 
class LowerResBlitTexture; template <> void RegisterClass<LowerResBlitTexture>();
class ProceduralTexture; 
class RenderTexture; template <> void RegisterClass<RenderTexture>();
class CustomRenderTexture; 
class SparseTexture; 
class Texture2D; template <> void RegisterClass<Texture2D>();
class Cubemap; template <> void RegisterClass<Cubemap>();
class Texture2DArray; template <> void RegisterClass<Texture2DArray>();
class Texture3D; template <> void RegisterClass<Texture3D>();
class VideoClip; 
class GameManager; template <> void RegisterClass<GameManager>();
class GlobalGameManager; template <> void RegisterClass<GlobalGameManager>();
class AudioManager; template <> void RegisterClass<AudioManager>();
class BuildSettings; template <> void RegisterClass<BuildSettings>();
class CloudWebServicesManager; template <> void RegisterClass<CloudWebServicesManager>();
class CrashReportManager; 
class DelayedCallManager; template <> void RegisterClass<DelayedCallManager>();
class GraphicsSettings; template <> void RegisterClass<GraphicsSettings>();
class InputManager; template <> void RegisterClass<InputManager>();
class MasterServerInterface; template <> void RegisterClass<MasterServerInterface>();
class MonoManager; template <> void RegisterClass<MonoManager>();
class NavMeshProjectSettings; 
class NetworkManager; template <> void RegisterClass<NetworkManager>();
class PerformanceReportingManager; 
class Physics2DSettings; template <> void RegisterClass<Physics2DSettings>();
class PhysicsManager; template <> void RegisterClass<PhysicsManager>();
class PlayerSettings; template <> void RegisterClass<PlayerSettings>();
class QualitySettings; template <> void RegisterClass<QualitySettings>();
class ResourceManager; template <> void RegisterClass<ResourceManager>();
class RuntimeInitializeOnLoadManager; template <> void RegisterClass<RuntimeInitializeOnLoadManager>();
class ScriptMapper; template <> void RegisterClass<ScriptMapper>();
class TagManager; template <> void RegisterClass<TagManager>();
class TimeManager; template <> void RegisterClass<TimeManager>();
class UnityAnalyticsManager; 
class UnityConnectSettings; template <> void RegisterClass<UnityConnectSettings>();
class LevelGameManager; template <> void RegisterClass<LevelGameManager>();
class LightmapSettings; template <> void RegisterClass<LightmapSettings>();
class NavMeshSettings; 
class OcclusionCullingSettings; 
class RenderSettings; template <> void RegisterClass<RenderSettings>();
class RenderPassAttachment; 

void RegisterAllClasses()
{
void RegisterBuiltinTypes();
RegisterBuiltinTypes();
	//Total: 82 non stripped classes
	//0. ParticleSystem
	RegisterClass<ParticleSystem>();
	//1. Unity::Component
	RegisterClass<Unity::Component>();
	//2. EditorExtension
	RegisterClass<EditorExtension>();
	//3. Behaviour
	RegisterClass<Behaviour>();
	//4. Camera
	RegisterClass<Camera>();
	//5. GameObject
	RegisterClass<GameObject>();
	//6. SkinnedMeshRenderer
	RegisterClass<SkinnedMeshRenderer>();
	//7. Renderer
	RegisterClass<Renderer>();
	//8. TrailRenderer
	RegisterClass<TrailRenderer>();
	//9. GUIElement
	RegisterClass<GUIElement>();
	//10. GUILayer
	RegisterClass<GUILayer>();
	//11. MonoBehaviour
	RegisterClass<MonoBehaviour>();
	//12. Shader
	RegisterClass<Shader>();
	//13. NamedObject
	RegisterClass<NamedObject>();
	//14. Material
	RegisterClass<Material>();
	//15. Sprite
	RegisterClass<Sprite>();
	//16. SpriteRenderer
	RegisterClass<SpriteRenderer>();
	//17. Texture
	RegisterClass<Texture>();
	//18. Texture2D
	RegisterClass<Texture2D>();
	//19. RenderTexture
	RegisterClass<RenderTexture>();
	//20. Transform
	RegisterClass<Transform>();
	//21. UI::RectTransform
	RegisterClass<UI::RectTransform>();
	//22. Mesh
	RegisterClass<Mesh>();
	//23. Rigidbody
	RegisterClass<Rigidbody>();
	//24. Collider
	RegisterClass<Collider>();
	//25. CapsuleCollider
	RegisterClass<CapsuleCollider>();
	//26. Animator
	RegisterClass<Animator>();
	//27. TextRendering::Font
	RegisterClass<TextRendering::Font>();
	//28. UI::Canvas
	RegisterClass<UI::Canvas>();
	//29. UI::CanvasGroup
	RegisterClass<UI::CanvasGroup>();
	//30. UI::CanvasRenderer
	RegisterClass<UI::CanvasRenderer>();
	//31. AudioClip
	RegisterClass<AudioClip>();
	//32. SampleClip
	RegisterClass<SampleClip>();
	//33. AudioListener
	RegisterClass<AudioListener>();
	//34. AudioBehaviour
	RegisterClass<AudioBehaviour>();
	//35. AudioSource
	RegisterClass<AudioSource>();
	//36. Collider2D
	RegisterClass<Collider2D>();
	//37. MeshRenderer
	RegisterClass<MeshRenderer>();
	//38. MeshCollider
	RegisterClass<MeshCollider>();
	//39. BoxCollider
	RegisterClass<BoxCollider>();
	//40. BoxCollider2D
	RegisterClass<BoxCollider2D>();
	//41. PreloadData
	RegisterClass<PreloadData>();
	//42. Cubemap
	RegisterClass<Cubemap>();
	//43. Texture3D
	RegisterClass<Texture3D>();
	//44. Texture2DArray
	RegisterClass<Texture2DArray>();
	//45. MeshFilter
	RegisterClass<MeshFilter>();
	//46. LowerResBlitTexture
	RegisterClass<LowerResBlitTexture>();
	//47. BuildSettings
	RegisterClass<BuildSettings>();
	//48. GlobalGameManager
	RegisterClass<GlobalGameManager>();
	//49. GameManager
	RegisterClass<GameManager>();
	//50. DelayedCallManager
	RegisterClass<DelayedCallManager>();
	//51. GraphicsSettings
	RegisterClass<GraphicsSettings>();
	//52. InputManager
	RegisterClass<InputManager>();
	//53. PlayerSettings
	RegisterClass<PlayerSettings>();
	//54. ResourceManager
	RegisterClass<ResourceManager>();
	//55. RuntimeInitializeOnLoadManager
	RegisterClass<RuntimeInitializeOnLoadManager>();
	//56. ScriptMapper
	RegisterClass<ScriptMapper>();
	//57. TagManager
	RegisterClass<TagManager>();
	//58. TimeManager
	RegisterClass<TimeManager>();
	//59. QualitySettings
	RegisterClass<QualitySettings>();
	//60. MonoManager
	RegisterClass<MonoManager>();
	//61. MasterServerInterface
	RegisterClass<MasterServerInterface>();
	//62. NetworkManager
	RegisterClass<NetworkManager>();
	//63. MonoScript
	RegisterClass<MonoScript>();
	//64. TextAsset
	RegisterClass<TextAsset>();
	//65. PhysicsManager
	RegisterClass<PhysicsManager>();
	//66. UnityConnectSettings
	RegisterClass<UnityConnectSettings>();
	//67. CloudWebServicesManager
	RegisterClass<CloudWebServicesManager>();
	//68. AudioManager
	RegisterClass<AudioManager>();
	//69. Physics2DSettings
	RegisterClass<Physics2DSettings>();
	//70. RuntimeAnimatorController
	RegisterClass<RuntimeAnimatorController>();
	//71. AnimatorController
	RegisterClass<AnimatorController>();
	//72. FlareLayer
	RegisterClass<FlareLayer>();
	//73. LightProbes
	RegisterClass<LightProbes>();
	//74. RenderSettings
	RegisterClass<RenderSettings>();
	//75. LevelGameManager
	RegisterClass<LevelGameManager>();
	//76. LightmapSettings
	RegisterClass<LightmapSettings>();
	//77. AnimationClip
	RegisterClass<AnimationClip>();
	//78. Motion
	RegisterClass<Motion>();
	//79. SphereCollider
	RegisterClass<SphereCollider>();
	//80. Light
	RegisterClass<Light>();
	//81. ParticleSystemRenderer
	RegisterClass<ParticleSystemRenderer>();

}
