using UnityEngine;

public class Missile_new : MonoBehaviour
{
    public float thrustMagnitude = 100f;
    public float burnTime = 5f;
    public float dragCoefficient = 0.2f;
    public LineRenderer trajectoryLinePrefab;
    public int numSimulations = 10; // Change to 10 simulations
    public float targetRadius = 5f;
    public Transform target; // Target object

    private float ro = 1.2f;
    private float A = Mathf.PI * Mathf.Pow(0.2f, 2);
    private float g = 9.81f;
    public bool isSimulate=false;

    void Start()
    {
        SimulateTrajectories(target);
    }
    
    void Update()
    {
        if(isSimulate) {
            isSimulate=false;
            Debug.LogError("Yes amn");
            SimulateTrajectories(target);
        }
    }

    void SimulateTrajectories(Transform target)
    {
        for (int i = 0; i < numSimulations; i++)
        {
            Vector3 startPosition = GetRandomPosition();
            SimulateTrajectory(startPosition, target.position);
        }
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(0f, 2f);
        float randomY = Random.Range(0f, 2f);
        float randomZ = Random.Range(0f, 2f);
        return new Vector3(randomX, randomY, randomZ);
    }

    void SimulateTrajectory(Vector3 startPosition, Vector3 targetPosition)
    {
        GameObject lineObject = new GameObject("TrajectoryLine");
        LineRenderer trajectoryLine = lineObject.AddComponent<LineRenderer>();
        trajectoryLine.material = new Material(Shader.Find("Sprites/Default"));
        trajectoryLine.startColor = Color.red;
        trajectoryLine.endColor = Color.red;
        trajectoryLine.startWidth = 0.1f;
        trajectoryLine.endWidth = 0.1f;
        trajectoryLine.positionCount = 2;
        trajectoryLine.SetPosition(0, startPosition);
        trajectoryLine.SetPosition(1, targetPosition);

        Rigidbody rb = new GameObject("Missile").AddComponent<Rigidbody>();
        rb.position = startPosition;
        rb.velocity = Vector3.zero;

        double dt = 0.5;
        double t = 0;

        // Random theta and phi
        float theta = Random.Range(0f, Mathf.PI * 2); // Angle in xy-plane
        float phi = Random.Range(0f, Mathf.PI); // Angle from z-axis

        Vector3 targetDirection = new Vector3(Mathf.Cos(theta) * Mathf.Sin(phi), Mathf.Sin(theta) * Mathf.Sin(phi), Mathf.Cos(phi));

        while (Vector3.Distance(rb.position, targetPosition) > targetRadius)
        {
            Debug.LogError(Vector3.Distance(rb.position, targetPosition));
            if(t>10) {
                break;
            }
            double[] thrust = Thrust(t, targetDirection);

            double Vmag = rb.velocity.magnitude;
            double m = rb.mass;
            double Cd = DragCoefficient(Vmag);

            Vector3 acceleration = new Vector3((float)(thrust[0] / m - (Cd * ro * A / (2 * m)) * rb.velocity.x * Vmag),
                                                (float)(thrust[1] / m - (Cd * ro * A / (2 * m)) * rb.velocity.y * Vmag),
                                                (float)(thrust[2] / m - (Cd * ro * A / (2 * m)) * rb.velocity.z * Vmag - g));

            rb.velocity += acceleration * (float)dt;
            rb.position += rb.velocity * (float)dt;

            t += dt;
        }

        Debug.Log("Simulation completed. Final position: " + rb.position);
    }

    double[] Thrust(double t, Vector3 targetDirection)
    {
        double[] thrust = new double[3];

        if (t <= burnTime)
        {
            // Follow target direction during burn time
            thrust[0] = thrustMagnitude * targetDirection.x;
            thrust[1] = thrustMagnitude * targetDirection.y;
            thrust[2] = thrustMagnitude * targetDirection.z;
        }
        else
        {
            // Stop thrusting after burn time
            thrust[0] = 0;
            thrust[1] = 0;
            thrust[2] = 0;
        }

        return thrust;
    }

    double DragCoefficient(double Vmag)
    {
        double Ma = Vmag / 340;

        if (Ma >= 0 && Ma < 0.9)
        {
            return 0.2;
        }
        else if (Ma < 1.1)
        {
            return 0.2 + 0.075 / 0.2 * (Ma - 0.9);
        }
        else if (Ma < 3)
        {
            return 0.275 - 0.075 / 1.9 * (Ma - 1.1);
        }
        else if (Ma > 3)
        {
            return 0.2;
        }
        else
        {
            Debug.LogError("Invalid Vmag to compute drag coefficient");
            return double.NaN;
        }
    }
}
