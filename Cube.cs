using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour 
{

    public Sprite[] sprites;
    public Material material;

    private MeshFilter filter;
    private Mesh mesh;
    private Renderer renderer;

    Rect GetSpriteUV(int i)
    {
        Rect rect = sprites[i].textureRect;
        float h = sprites[i].texture.height;
        float w = sprites[i].texture.width;
        rect.x /= w;
        rect.width /= w;
        rect.y /= h;
        rect.height /= h;
        return rect;
    }


     Vector4[] GetSpriteUVs(int[] indexs)
    {
        Vector4[] v = new Vector4[6];
        for (int i = 0; i < 6; ++i)
        {
            Rect r = GetSpriteUV(indexs[i]);
            v[i].x = r.x;
            v[i].y = r.y;
            v[i].z = r.x + r.width;
            v[i].w = r.y + r.height;
        }
        return v;
    }
    public void set_spcolor(int tmp,int tmp2)
    {


    }

    public void Create(int[] indexs)
    {
        filter = gameObject.AddComponent<MeshFilter>();
        mesh = new Mesh();
        filter.mesh = mesh;
        renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = material;
        renderer.material.mainTexture = sprites[0].texture;

        mesh.vertices = new Vector3[]
        {
            // front
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),

            // back
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),

            // top
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),

            // bottom
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),

            // left
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),

            // right
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
        };

        mesh.normals = new Vector3[]
        {
            // front
            new Vector3(0, 0, -1),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, -1),

            // back
            new Vector3(0, 0, 1),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, 1),

            // top
            new Vector3(0, 1, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 1, 0),

            // bottom
            new Vector3(0, -1, 0),
            new Vector3(0, -1, 0),
            new Vector3(0, -1, 0),
            new Vector3(0, -1, 0),

            // left
            new Vector3(-1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(-1, 0, 0),

            // right
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 0),
        };

        Vector4[] v = GetSpriteUVs(indexs);
        mesh.uv = new Vector2[]
        {
            // front
            new Vector2(v[0].z, v[0].w), 
            new Vector2(v[0].z, v[0].y), 
            new Vector2(v[0].x, v[0].y), 
            new Vector2(v[0].x, v[0].w),

            // back
            new Vector2(v[1].x, v[1].w), 
            new Vector2(v[1].x, v[1].y), 
            new Vector2(v[1].z, v[1].y), 
            new Vector2(v[1].z, v[1].w),

            // top
            new Vector2(v[2].z, v[2].w), 
            new Vector2(v[2].z, v[2].y), 
            new Vector2(v[2].x, v[2].y), 
            new Vector2(v[2].x, v[2].w),

            // bottom
            new Vector2(v[3].z, v[3].y), 
            new Vector2(v[3].z, v[3].w), 
            new Vector2(v[3].x, v[3].w), 
            new Vector2(v[3].x, v[3].y),

            // left
            new Vector2(v[4].x, v[4].w), 
            new Vector2(v[4].z, v[4].w), 
            new Vector2(v[4].z, v[4].y),
            new Vector2(v[4].x, v[4].y), 

            // right
            new Vector2(v[5].z, v[5].w),
            new Vector2(v[5].x, v[5].w), 
            new Vector2(v[5].x, v[5].y), 
            new Vector2(v[5].z, v[5].y), 
           
        };

        mesh.SetIndices(new int[]
        {
            0, 1, 2, 0, 2, 3,
            4, 7, 6, 4, 6, 5,
            8, 9, 10, 8, 10, 11,
            12, 15, 14, 12, 14, 13,
            16, 17, 18, 16, 18, 19,
            20, 23, 22, 20, 22, 21,
        }, MeshTopology.Triangles, 0);
        
    }
    public void all_clear()
    {
        mesh.Clear();
    }
    public void SetColor(Color color)
    {
        renderer.material.color = color;
    }

}
