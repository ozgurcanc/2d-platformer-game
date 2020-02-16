using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cyclone
{
    
public class Vector2D  
{
    
    private float pad;

    public float x;
    public float y;

    public Vector2D()
    {
        x = 0.0f ;
        y = 0.0f ;
    }

    public Vector2D( float  _x, float _y)
    {
        x = _x;
        y = _y;
    }

    public Vector2D(Vector2D v)
    {
        x = v.x;
        y = v.y;
    }

    public void Invert()
    {
        x = -x;
        y = -y;
    }

    public float Magnitude 
    {
        get 
        {
            return Mathf.Sqrt((float)((double)x*(double)x+(double)y*(double)y));
        }
    }

    public float SqrMagnitude
    {
        get
        {
            return (float)((double)x*(double)x+(double)y*(double)y);
        }
    }
    
    public void Normalize()
    {
        float magnitude = this.Magnitude;
        if(magnitude > 0)
        {
            this.x /= magnitude;
            this.y /= magnitude;
        }
    }

    public Vector2D Normalized
    {
        get
        {
            Vector2D copy = new Vector2D(this.x,this.y);
            copy.Normalize();
            return copy;
        }
    } 

    public void AddScaledVector(Vector2D a,float d)
    {
        x += a.x * d;
        y += a.y * d;
    }

    public float DotProduct(Vector2D a)
    {
        return (x*a.x + y * a.y) ;
    }

    public static float operator *(Vector2D a, Vector2D b)
    {
        return (a.x * b.x + a.y * b.y);
    }

    public Vector2D Perpendicular
    {
        get
        {
            return new Vector2D(-y,x);
        }
    }

    public float Distance(Vector2D a)
    {
        return (this -a).Magnitude;
    }

    public static float Distance(Vector2D a, Vector2D b)
    {
        return (a-b).Magnitude;
    }
  
    public Vector2D ComponentProduct(Vector2D a)
    {
        return new Vector2D(x* a.x , y* a.y);
    }
   
    public void ComponentProductUpdate(Vector2D a)
    {
        x *= a.x;
        y *= a.y;
    }
 

    public static Vector2D operator +(Vector2D a, Vector2D b)
    {
        return new Vector2D(a.x + b.x, a.y + b.y);
    }

    public static Vector2D operator -(Vector2D a, Vector2D b)
    {
        return new Vector2D(a.x - b.x, a.y - b.y);
    }

    public static Vector2D operator -(Vector2D a)
    {
        return new Vector2D(-a.x, -a.y);
    }

    public static Vector2D operator/(Vector2D a, float d)
    {
        return new Vector2D(a.x / d, a.y / d);
    }
    
    public static Vector2D operator *(Vector2D a, float d)
    {
        return new Vector2D(a.x * d, a.y * d);
    }

    public static Vector2D operator *(float d, Vector2D a)
    {
        return new Vector2D(a.x * d, a.y * d);
    }

    public static bool operator ==(Vector2D a, Vector2D b)
    {
        return (double) (a - b).SqrMagnitude < 9.99999943962493E-11;
    }

    public static bool operator !=(Vector2D a, Vector2D b)
    {
        return (double) (a - b).SqrMagnitude >= 9.99999943962493E-11;
    }

    public override bool Equals(object other)
    {
        if (!(other is Vector2D))
            return false;
        Vector2D vector2 = (Vector2D) other;
        if (this.x.Equals(vector2.x))
            return this.y.Equals(vector2.y);
        return false;
    }

    public override string ToString()
    {
        return "("+ x.ToString() +"," + y.ToString() + ")";
    }

    public static implicit operator Vector2D(Vector3 a)
    {
        return new Vector2D(a.x, a.y);
    }

    public static implicit operator Vector3(Vector2D a)
    {
        return new Vector3(a.x, a.y, 0.0f);
    }  

}

}
