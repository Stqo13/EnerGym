namespace EnerGym.Common
{
    public static class ApplicationConstraints
    {
        public static class MembershipPlanConstraints 
        {
            public const int DescriptionMaxLenght = 200;
        }

        public static class GymClassConstraints 
        {
            public const int ClassNameMinLength = 20;
            public const int ClassNameMaxLength = 150;
            public const int DescriptionMaxLength = 200;
            public const int InstructorNameMinLength = 20;
            public const int InstructorNameMaxLength = 70;
        }

        public static class ApplicationUserConstraints
        {
            public const int FirstNameMinLength = 5;
            public const int FirstNameMaxLength = 70;
            public const int LastNameMinLength = 5;
            public const int LastNameMaxLength = 70;

            public const string AdminRole = "Admin";
            public const string InstructorRole = "Instructor";
            public const string GymMemberRole = "GymMember";
            /// <summary>
            /// Role that does not have a plan active
            /// </summary>
            public const string UserRole = "User";
        }

        public static class WorkoutRoutineConstraints
        {
            public const int ExerciseNameMinLength = 5;
            public const int ExerciseNameMaxLength = 100;
            public const int DescriptionMaxLength = 300;
        }

        public static class WorkoutPlanConstraints
        {
            public const int PlanNameMinLength = 5;
            public const int PlanNameMaxLength = 70;
            public const int DescriptionMaxLength = 300;
        }

        public static class ProgressConstraints
        {
            public const int NotesMaxLength = 1000;
        }
    }
}
