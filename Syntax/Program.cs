using System;
class HelloWorld {
	/*!
	    I used Camel case for variable names and Pascal case for method,enum,class names.
		You might not like it but this is how I code.
	*/

	public enum ProjectStatus {
		NotStarted,
		InProgress,
		Completed

	}

	internal class Project {
		public required string title { get; set; }
		public int progress{get;set; }
		public int bugCount{get; set;}
		public ProjectStatus status{ get; private set;}

		public void UpdateStatus() {
			if (progress == 0) status=ProjectStatus.NotStarted;
			else if (progress == 100 && bugCount == 0) status=ProjectStatus.Completed;
			else status=ProjectStatus.InProgress;
		}
	}

	internal class Worker {
		protected string name;
		protected int skill;
		public int energy{get; protected set;}

		public Worker(string name,int skill, int energy) {
			this.name = name;
			this.skill = skill;
			this.energy = energy;
		}

		protected void Rest() {
			energy = (energy < 80) ? energy+20 : 100;
		}
		protected bool CanWork() {
			return energy>=30;
		}

		public virtual void GetInfo() {
			Console.WriteLine($"\nName: {this.name}\nEnergy:{this.energy}");
		}

	}

	internal class Programmer : Worker {

		private int debugSkill;

		public Programmer (string name,int skill, int energy,int debugSkill) : base(name,skill,energy) {
			this.debugSkill = debugSkill;
		}

		public void Develop(Project project) {
			if (!(CanWork()) || project.status == ProjectStatus.Completed ) {
				// Rest(); //! It would be better to use Rest() function for the case a worker cannot work
				return;
			}

			project.progress = Math.Min(100, project.progress + skill * 2);
			this.energy = Math.Max(0, energy - 20);

			if(debugSkill <7) project.bugCount+=1;


			project.UpdateStatus();

		}
		public void FixBug(Project project) {
			if (!(CanWork()) || project.bugCount == 0 ) {
				// Rest(); //! It would be better to use Rest() function for the case a worker cannot work
				return;
			}


			project.bugCount = Math.Max(0, project.bugCount - 1);
			this.energy =Math.Max(0, energy - 10);
			project.UpdateStatus();

		}

		public override void GetInfo() {
			base.GetInfo();
			Console.WriteLine($"Position: Programmer");

		}
	}

	internal class Artist : Worker {

		private int creativity;

		public Artist (string name,int skill, int energy,int creativity) : base(name,skill,energy) {
			this.creativity = creativity;
		}

		public void Design(Project project) {
			if (!(CanWork()) || project.status == ProjectStatus.Completed ) {
				// Rest(); //! It would be better to use Rest() function for the case a worker cannot work
				return;
			}

			int progressGain = skill + creativity;
			if (project.bugCount > 5)  progressGain -= 2 ;
			project.progress = Math.Min(100, project.progress + progressGain);
			this.energy = Math.Max(0, energy - 15);
			project.UpdateStatus();

		}

		public override void GetInfo() {
			base.GetInfo();
			Console.WriteLine($"Position: Artist");

		}

	}

	internal class Team {
		private Project project;
		private int deadline;
		private Programmer p1;
		private Programmer p2;
		private Artist a1;
		private Artist a2;

		public Team(Project project, int deadline, Programmer p1, Programmer p2, Artist a1, Artist a2) {
			this.project = project;
			this.deadline = deadline;
			this.p1 = p1;
			this.p2 = p2;
			this.a1 = a1;
			this.a2 = a2;
		}

		public void StartProject() {
			for (int day = 1; day <= deadline; day++) {
				Console.WriteLine($"\nDay: {day} ||||| Project: {project.title}");

				if (project.progress < 100) {
					if (day % 2 == 0) {
						p1.Develop(project);
						a1.Design(project);
					} else {
						p2.Develop(project);
						a2.Design(project);
					}
				}

				if (project.bugCount > 0) {
					if (day % 2 == 0)
						p1.FixBug(project);
					else
						p2.FixBug(project);
				}

				if (day % 2 == 0) {
					p1.GetInfo();
					a1.GetInfo();
				} else {
					p2.GetInfo();
					a2.GetInfo();
				}

				Console.WriteLine($"\nPRogress: {project.progress}%");
				Console.WriteLine($"Bug count: {project.bugCount}");
				Console.WriteLine($"Project status: {project.status}");

				if (project.status == ProjectStatus.Completed) {
					Console.BackgroundColor = ConsoleColor.Green;
					Console.WriteLine("\n\nProject done");
					Console.ResetColor();
					return;
				}
			}

			Console.BackgroundColor = ConsoleColor.Red;
			Console.WriteLine("\n\nDeadline exceeded! Project has failed");
			Console.ResetColor();
		}
	}

	static void Main() {
		Project project1 = new Project { title = "Leo Bank" };
		Programmer programmerKamran = new Programmer("Kamran", 7, 49, 4);
		Programmer programmerEhmed = new Programmer("Ehmed", 9, 85, 8);
		Artist artistKemale = new Artist("Kemale", 6, 21, 6);
		Artist artistNilay = new Artist("Nilay", 0, 0, 0);
		int deadline1 = 7;

		Team team = new Team(project1,
                             deadline1,
		                     programmerKamran,
                             programmerEhmed,
		                     artistKemale,
                             artistNilay
                             );
		team.StartProject();

		Project project2 = new Project { title = "Mac OS" };
		Programmer programmerApple1 = new Programmer("Tim Cook", 9, 52, 6);
		Programmer programmerApple2 = new Programmer("Steve Wozniak", 9, 91, 9);
		Artist artistApple1 = new Artist("Steve Jobs", 8, 88,9 );
		Artist artistApple2 = new Artist("John", 5, 56, 7);
		int deadline2 = 14;

		Team teamApple = new Team(project2,
                                  deadline2,
		                          programmerApple1,
                                  programmerApple2,
		                          artistApple1,
                                  artistApple2
                                  );
		teamApple.StartProject();
	}
}