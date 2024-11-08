namespace EnerGym.Common
{
    public static class ApplicationConstraints
    {
        public static class MembershipPlanConstraints 
        {
            public const int DescriptionMinLenght = 20;
            public const int DescriptionMaxLenght = 200;
        }

        public static class GymClassConstraints 
        {
            public const int ClassNameMinLenght = 20;
            public const int ClassNameMaxLenght = 150;
            public const int DescriptionMinLenght = 20;
            public const int DescriptionMaxLenght = 200;
            public const int InstructorNameMinLenght = 20;
            public const int InstructorNameMaxLenght = 70;
        }

        public static class ApplicationUserConstraints
        {
            public const int FirstNameMinLenght = 20;
            public const int FirstNameMaxLenght = 70;
            public const int LastNameMinLenght = 20;
            public const int LastNameMaxLenght = 70;
        }

        public static class WorkoutRoutineConstraints
        {
            public const int ExerciseNameMinLenght = 20;
            public const int ExerciseNameMaxLenght = 100;
            public const int DescriptionMinLenght = 100;
            public const int DescriptionMaxLenght = 300;
        }

        public static class WorkoutPlanConstraints
        {
            public const int PlanNameMinLenght = 20;
            public const int PlanNameMaxLenght = 70;
            public const int DescriptionMinLenght = 100;
            public const int DescriptionMaxLenght = 300;
        }

        public static class ProgressConstraints
        {
            public const int NotesMinLenght = 100;
            public const int NotesMaxLenght = 1000;
        }
    }
}
