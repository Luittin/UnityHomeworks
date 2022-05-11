using System;
using System.Collections.Generic;
using UnityEngine;

public class CreateBallLevel
{
    private List<Ball> _balls;
    
    private event Action DecreateLife;

    public void CreateBall(BallStats ballStats, LevelManager levelManager)
    {
        DecreateLife = levelManager.OnDecreaseLife;
        
        _balls = new List<Ball>(); 

        Ball ball = ballStats.GetComponent<Ball>();
        //ball.OnCollision += audioManager.OnPlayAudio;
        ball.DepartureAbroad += OnDepartureAbroadBall;
        _balls.Add(ball);
    }
    
    private void OnDepartureAbroadBall(Ball ball)
    {
        if (_balls.Count > 1)
        {
            ball.StopBall();
            MonoBehaviour.Destroy(ball.gameObject);
        }
        else
        {
            ball.StopBall();
            ball.MoveStartPosition();

            DecreateLife?.Invoke();
        }
    }
}