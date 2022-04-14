//using Unity.Mathematics;

//public class Capsule {
//    public float3 Pos1 { get; set; }
//    public float3 Pos2 { get; set; }
//    public float Radius { get; set; }

//    public float3 Direction {
//        get {
//            return Pos2 - Pos1;
//        }
//    }

//    public float3 Center {
//        get {
//            return (Pos1 + Pos2) / 2f;
//        }
//    }

//    public Capsule(float3 p1, float3 p2, float radius) {
//        Pos1 = p1;
//        Pos2 = p2;
//        Radius = radius;
//    }

//    public Capsule(float3 center, float3 direction, float length, float radius) {
//        float3 d2 = math.normalize(direction) * length / 2f;

//        Pos1 = center - d2;
//        Pos2 = center + d2;
//        Radius = radius;
//    }

//    public bool Contains(float3 point) {
//        if (Sphere.Contains(Pos1, Radius, point) || Sphere.Contains(Pos2, Radius, point)) {
//            return true;
//        }

//        float3 pDir = point - Pos1;
        
//        float lengthsq = Direction.x * Direction.x + Direction.y * Direction.y + Direction.z * Direction.z;
//        float dot = math.dot(Direction, pDir);
//        if (dot < 0f || dot > lengthsq) return false;

//        float dsq = pDir.x * pDir.x + pDir.y * pDir.y + pDir.z * pDir.z - dot * dot / lengthsq;

//        if (dsq > Radius * Radius) {
//            return false;
//        }
//        else {
//            return true;
//        }
//    }
//}
  
 
//  public class Sphere {
//    public float3 Center { get; set; }
//    public float Radius { get; set; }

//    public Sphere(float3 position, float rad) {
//        Center = position;
//        Radius = rad;
//    }

//    public bool Contains(float3 point) {
//        return Contains(Center, Radius, point);
//    }

//    public static bool Contains(Sphere sphere, float3 point) {
//        return Contains(sphere.Center, sphere.Radius, point);
//    }

//    public static bool Contains(float3 spherePos, float radius, float3 point) {
//        float3 pO = spherePos - point; // (point Offset
//        float dist = pO.x * pO.x + pO.y * pO.y + pO.z*pO.z;
//        return dist <= radius * radius;
//    }
//}
