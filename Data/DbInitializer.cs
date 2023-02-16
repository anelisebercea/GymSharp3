using Microsoft.EntityFrameworkCore;
using GymSharp.Models;


namespace GymSharp.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GymContext(serviceProvider.GetRequiredService<DbContextOptions<GymContext>>()))
            { }

                /*
                if (context.Exercises.Any())
                {
                    return;
                }
                context.Exercises.AddRange(
                new Exercise
                {
                    ExerciseName = "Bicep Curl",
                    TrainerId = 7,
                    Description = "Isolation exercise for biceps",
                    DifficultyLevel = "easy",
                },
                new Exercise
                {
                    ExerciseName = "Tricep Dips",
                    TrainerId = 9,
                    Description = "Isolation exercise for triceps",
                    DifficultyLevel = "moderate",
                   
                },
                new Exercise
                {
                    ExerciseName = "Hammer Curl",
                    TrainerId = 3,
                    Description = "Isolation exercise for biceps",
                    DifficultyLevel = "moderate",

                },
                new Exercise
                {
                    ExerciseName = "Deadlift",
                    TrainerId = 1,
                    Description = "A compound movement that works your lower back, glutes, hamstrings, and core.",
                    DifficultyLevel = "hard",
                },
                new Exercise
                {
                    ExerciseName = "Bench Press",
                    TrainerId = 3,
                    Description = "A compound movement that works your chest, triceps, and shoulders.",
                    DifficultyLevel = "moderate",

                },
                new Exercise
                {
                    ExerciseName = "Cable Fly",
                    TrainerId = 2,

                    Description = "Isolation exercise for chest",
                    DifficultyLevel = "easy",
                },
                new Exercise
                {
                    ExerciseName = "Incline bench press",
                    TrainerId = 3,
                    Description = "Compound exercise for upper chest",
                    DifficultyLevel = "moderate",

                },
                new Exercise
                {
                    ExerciseName = "Dumbbell press",
                    TrainerId = 1,
                    Description = "Isolation exercise for chest",
                    DifficultyLevel = "hard",

                },
                new Exercise
                {
                    ExerciseName = "Cable Crossover",
                    TrainerId = 8,

                    Description = "Isolation exercise for chest",
                    DifficultyLevel = "moderate",


                },
                new Exercise
                {
                    ExerciseName = "Push ups",
                    TrainerId = 4,
                    Description = "Bodyweight exercise for chest",
                    DifficultyLevel = "easy",

                },
                new Exercise
                {
                    ExerciseName = "Dumbbell Fly",
                    TrainerId = 3,

                    Description = "Isolation exercise for chest",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Incline Dumbbell Fly",
                    TrainerId = 1,
                    Description = "Isolation exercise for upper chest",
                    DifficultyLevel = "hard",

                },
                new Exercise
                {
                    ExerciseName = "Barbell press",
                    TrainerId = 1,

                    Description = "Compound exercise for chest",
                    DifficultyLevel = "hard",


                },
                new Exercise
                {
                    ExerciseName = "Lunges",
                    TrainerId = 2,

                    Description = "Compound exercise for legs and glutes",
                    DifficultyLevel = "easy",

                },
                new Exercise
                {
                    ExerciseName = "Squat",
                    TrainerId = 8,

                    Description = "Compound exercise for legs",
                    DifficultyLevel = "moderate",


                },
                new Exercise
                {
                    ExerciseName = "Leg press",
                    TrainerId = 1,

                    Description = "Compound exercise for legs and glutes",
                    DifficultyLevel = "hard",

                },
                new Exercise
                {
                    ExerciseName = "Calf Raise",
                    TrainerId = 7,

                    Description = "Isolation exercise for calves",
                    DifficultyLevel = "easy",

                },
                new Exercise
                {
                    ExerciseName = "Step-up",
                    TrainerId = 4,

                    Description = "Compound exercise for legs and glutes",
                    DifficultyLevel = "easy",
                },
                new Exercise
                {
                    ExerciseName = "Leg Extension",
                    TrainerId = 2,

                    Description = "Isolation exercise for quadriceps",
                    DifficultyLevel = "easy",

                },
                new Exercise
                {
                    ExerciseName = "Leg Curl",
                    TrainerId = 2,

                    Description = "Isolation exercise for hamstrings",
                    DifficultyLevel = "easy",

                },
                new Exercise
                {
                    ExerciseName = "Box Jump",
                    TrainerId = 9,

                    Description = "Compound exercise for legs and core",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Reverse lunge",
                    TrainerId = 2,

                    Description = "Compound exercise for legs and glutes",
                    DifficultyLevel = "easy",
                },
                new Exercise
                {
                    ExerciseName = "Pull-ups",
                    TrainerId = 9,

                    Description = "Compound exercise for back and biceps",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Shoulder Press",
                    TrainerId = 1,

                    Description = "Compound exercise for shoulders and triceps",
                    DifficultyLevel = "hard",
                },
                new Exercise
                {
                    ExerciseName = "Barbell Row",
                    Description = "Compound exercise for back and biceps",
                    DifficultyLevel = "moderate",
                    TrainerId = 3,
                },

                new Exercise
                {
                    ExerciseName = "Lat Pulldown",
                    TrainerId = 3,

                    Description = "Compound exercise for back and biceps",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Dumbbell Row",
                    TrainerId = 3,

                    Description = "Compound exercise for back and biceps",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Seated Cable Row",
                    TrainerId = 9,

                    Description = "Compound exercise for back and biceps",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Lateral Raise",
                    TrainerId = 2,

                    Description = "Isolation exercise for shoulders",
                    DifficultyLevel = "easy",
                },
                new Exercise
                {
                    ExerciseName = "Upright Row",
                    TrainerId = 6,

                    Description = "Compound exercise for shoulders and traps",
                    DifficultyLevel = "hard",
                },

                new Exercise
                {
                    ExerciseName = "Planks",
                    TrainerId = 8,

                    Description = "Isolation exercise for core",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Sit-ups",
                    TrainerId = 8,
                    Description = "Core exercise for the rectus abdominis",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Russian Twist",
                    TrainerId = 9,
                    Description = "Core exercise for the obliques",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Dead bug",
                    TrainerId = 5,
                    Description = "Core exercise for the rectus abdominis,  internal and external obliques",
                    DifficultyLevel = "hard",
                },
                new Exercise
                {
                    ExerciseName = "Side plank",
                    TrainerId = 5,

                    Description = "Core exercise for the rectus abdominis and obliques",
                    DifficultyLevel = "hard",
                },
                new Exercise
                {
                    ExerciseName = "Mountain climbers",
                    TrainerId = 6,
                    Description = "Compound exercise for the core and legs",
                    DifficultyLevel = "hard",
                },
                new Exercise
                {
                    ExerciseName = "Bicycle crunch",
                    TrainerId = 9,
                    Description = "Core exercise for the rectus abdominis and obliques",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Reverse crunch",
                    TrainerId = 5,
                    Description = "Core exercise for the rectus abdominis",
                    DifficultyLevel = "hard",
                },
                new Exercise
                {
                    ExerciseName = "Hanging leg raise",
                    TrainerId = 6,
                    Description = "Core exercise for the rectus abdominis,  internal and external obliques",
                    DifficultyLevel = "hard",
                },
                new Exercise
                {
                    ExerciseName = "Hip thrust",
                    TrainerId = 9,
                    Description = "Core exercise for the rectus abdominis,  glutes and hamstrings",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Reverse Fly",
                    TrainerId = 5,
                    Description = "Isolation exercise for shoulders",
                    DifficultyLevel = "hard",
                },
                new Exercise
                {
                    ExerciseName = "Tricep Pushdowns",
                    TrainerId = 3,
                    Description = "Isolation exercise for triceps",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Diamond Push ups",
                    TrainerId = 6,
                    Description = "Isolation exercise for triceps",
                    DifficultyLevel = "hard",
                },
                new Exercise
                {
                    ExerciseName = "Close-Grip Bench Press",
                    TrainerId = 3,
                    Description = "Compound exercise for triceps",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Tricep Pushdowns",
                    TrainerId = 3,
                    Description = "Isolation exercise for triceps",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Tricep Extension",
                    TrainerId = 3,
                    Description = "Isolation exercise for triceps",
                    DifficultyLevel = "moderate",
                },
                new Exercise
                {
                    ExerciseName = "Tricep Kickbacks",
                    TrainerId = 1,
                    Description = "Isolation exercise for triceps",
                    DifficultyLevel = "hard",
                },
                new Exercise
                {
                    ExerciseName = "Barbell Close Grip Bench Press",
                    TrainerId = 3,
                    Description = "Compound exercise for triceps",
                    DifficultyLevel = "moderate",
                }
                );
                



                var measurements = new Measurement[]
                {
                    new Measurement{UserID=1,ExerciseID=1,Date=DateTime.Parse("2021-02-25"),BodyFatPercentage=27,Chest=90,Waist=86,Hips=97},

                };
                foreach (Measurement e in measurements)
                {
                    context.Measurements.Add(e);
                }
                context.SaveChanges();

                
                var muscleGroups = new MuscleGroup[]
                {

                    new MuscleGroup{Group="legs"},
                    new MuscleGroup{Group="arms"},
                    new MuscleGroup{Group="chest"},
                    new MuscleGroup{Group="back"},
                    new MuscleGroup{Group="shoulders"},
                    new MuscleGroup{Group="core"},
                    
                };
                foreach (MuscleGroup p in muscleGroups)
                {
                    context.MuscleGroups.Add(p);
                }
                context.SaveChanges();
                

                var muscleGroups = context.MuscleGroups;
                var exercises = context.Exercises;
                var workoutplans = new WorkoutPlan[]
                {
                    new WorkoutPlan {
                        ExerciseID = exercises.Single(c => c.ExerciseName == "..." ).ID,
                        MuscleGroupID = muscleGroups.Single(i => i.Group =="...").ID
                    },


                };
                foreach (WorkoutPlan pb in workoutplans)
                {
                    context.WorkoutPlans.Add(pb);
                }
                
                 


                var workoutplans = new WorkoutPlan[]
                {
                    new WorkoutPlan {
                        ExerciseID = 6,
                        MuscleGroupID = 1
                    },
                    new WorkoutPlan {
                        ExerciseID = 22,
                        MuscleGroupID = 1
                    },
                    new WorkoutPlan {
                        ExerciseID = 23,
                        MuscleGroupID = 1
                    },
                    new WorkoutPlan {
                        ExerciseID = 34,
                        MuscleGroupID = 1
                    },
                    new WorkoutPlan {
                        ExerciseID = 35,
                        MuscleGroupID = 1
                    },
                    new WorkoutPlan {
                        ExerciseID = 2,
                        MuscleGroupID = 2
                    },
                    new WorkoutPlan {
                        ExerciseID = 6,
                        MuscleGroupID = 2
                    },
                    new WorkoutPlan {
                        ExerciseID = 13,
                        MuscleGroupID = 2
                    },
                    new WorkoutPlan {
                        ExerciseID = 44,
                        MuscleGroupID = 2
                    },
                    new WorkoutPlan {
                        ExerciseID = 45,
                        MuscleGroupID = 2
                    },


                };
                foreach (WorkoutPlan pb in workoutplans)
                {
                    context.WorkoutPlans.Add(pb);
                }


                context.SaveChanges();
                */

            
        }

    }
}