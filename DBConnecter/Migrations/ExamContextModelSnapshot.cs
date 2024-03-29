﻿// <auto-generated />
using System;
using ExamWebEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DBConnecter.Migrations
{
    [DbContext(typeof(ExamContext))]
    partial class ExamContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ExamWebEF.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ChoiceAnswer")
                        .HasColumnType("int");

                    b.Property<string>("TxtAnswer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("ExamWebEF.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("classes");
                });

            modelBuilder.Entity("ExamWebEF.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<float>("Time")
                        .HasColumnType("real");

                    b.Property<int>("UserTeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserTeacherId");

                    b.ToTable("exams");
                });

            modelBuilder.Entity("ExamWebEF.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionContextId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("QuestionContextId")
                        .IsUnique();

                    b.ToTable("questions");
                });

            modelBuilder.Entity("ExamWebEF.QuestionChoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ChoiceA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChoiceB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChoiceC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChoiceD")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("QuestionChoice");
                });

            modelBuilder.Entity("ExamWebEF.QuestionContext", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AnswerId")
                        .HasColumnType("int");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<int>("DifficultyLevel")
                        .HasColumnType("int");

                    b.Property<int?>("QuestionChoiceId")
                        .HasColumnType("int");

                    b.Property<string>("Stem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId")
                        .IsUnique();

                    b.HasIndex("QuestionChoiceId")
                        .IsUnique()
                        .HasFilter("[QuestionChoiceId] IS NOT NULL");

                    b.ToTable("QuestionContext");
                });

            modelBuilder.Entity("ExamWebEF.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("StudentExamId")
                        .HasColumnType("int");

                    b.Property<int>("score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentExamId")
                        .IsUnique();

                    b.ToTable("Score");
                });

            modelBuilder.Entity("ExamWebEF.StudentExam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<int>("UserStudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("UserStudentId");

                    b.ToTable("studentExams");
                });

            modelBuilder.Entity("ExamWebEF.StudentQuestionAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ChoiceAnswer")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("TxtAnswer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserStudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserStudentId");

                    b.ToTable("StudentQuestionAnswers");
                });

            modelBuilder.Entity("ExamWebEF.UserStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("userStudents");
                });

            modelBuilder.Entity("ExamWebEF.UserStudentClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<int>("UserStudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("UserStudentId")
                        .IsUnique();

                    b.ToTable("userStudentClasses");
                });

            modelBuilder.Entity("ExamWebEF.UserTeacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("userTeachers");
                });

            modelBuilder.Entity("ExamWebEF.UserTeacherClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<int>("UserTeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("UserTeacherId");

                    b.ToTable("userTeacherClasses");
                });

            modelBuilder.Entity("ExamWebEF.Exam", b =>
                {
                    b.HasOne("ExamWebEF.UserTeacher", "userTeacher")
                        .WithMany("exams")
                        .HasForeignKey("UserTeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("userTeacher");
                });

            modelBuilder.Entity("ExamWebEF.Question", b =>
                {
                    b.HasOne("ExamWebEF.Exam", "exam")
                        .WithMany("questions")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamWebEF.QuestionContext", "questionContext")
                        .WithOne("question")
                        .HasForeignKey("ExamWebEF.Question", "QuestionContextId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("exam");

                    b.Navigation("questionContext");
                });

            modelBuilder.Entity("ExamWebEF.QuestionContext", b =>
                {
                    b.HasOne("ExamWebEF.Answer", "answer")
                        .WithOne("questionContext")
                        .HasForeignKey("ExamWebEF.QuestionContext", "AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamWebEF.QuestionChoice", "questionChoice")
                        .WithOne("questionContext")
                        .HasForeignKey("ExamWebEF.QuestionContext", "QuestionChoiceId");

                    b.Navigation("answer");

                    b.Navigation("questionChoice");
                });

            modelBuilder.Entity("ExamWebEF.Score", b =>
                {
                    b.HasOne("ExamWebEF.StudentExam", "studentExam")
                        .WithOne("score")
                        .HasForeignKey("ExamWebEF.Score", "StudentExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("studentExam");
                });

            modelBuilder.Entity("ExamWebEF.StudentExam", b =>
                {
                    b.HasOne("ExamWebEF.Exam", "exam")
                        .WithMany("studentExams")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamWebEF.UserStudent", "userStudent")
                        .WithMany("studentExams")
                        .HasForeignKey("UserStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("exam");

                    b.Navigation("userStudent");
                });

            modelBuilder.Entity("ExamWebEF.StudentQuestionAnswer", b =>
                {
                    b.HasOne("ExamWebEF.Question", "question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamWebEF.UserStudent", "userStudent")
                        .WithMany()
                        .HasForeignKey("UserStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("question");

                    b.Navigation("userStudent");
                });

            modelBuilder.Entity("ExamWebEF.UserStudentClass", b =>
                {
                    b.HasOne("ExamWebEF.Class", "classes")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamWebEF.UserStudent", "userStudent")
                        .WithOne("userStudentClass")
                        .HasForeignKey("ExamWebEF.UserStudentClass", "UserStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("classes");

                    b.Navigation("userStudent");
                });

            modelBuilder.Entity("ExamWebEF.UserTeacherClass", b =>
                {
                    b.HasOne("ExamWebEF.Class", "classes")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamWebEF.UserTeacher", "UserTeacher")
                        .WithMany("userTeacherClass")
                        .HasForeignKey("UserTeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserTeacher");

                    b.Navigation("classes");
                });

            modelBuilder.Entity("ExamWebEF.Answer", b =>
                {
                    b.Navigation("questionContext")
                        .IsRequired();
                });

            modelBuilder.Entity("ExamWebEF.Exam", b =>
                {
                    b.Navigation("questions");

                    b.Navigation("studentExams");
                });

            modelBuilder.Entity("ExamWebEF.QuestionChoice", b =>
                {
                    b.Navigation("questionContext")
                        .IsRequired();
                });

            modelBuilder.Entity("ExamWebEF.QuestionContext", b =>
                {
                    b.Navigation("question")
                        .IsRequired();
                });

            modelBuilder.Entity("ExamWebEF.StudentExam", b =>
                {
                    b.Navigation("score")
                        .IsRequired();
                });

            modelBuilder.Entity("ExamWebEF.UserStudent", b =>
                {
                    b.Navigation("studentExams");

                    b.Navigation("userStudentClass")
                        .IsRequired();
                });

            modelBuilder.Entity("ExamWebEF.UserTeacher", b =>
                {
                    b.Navigation("exams");

                    b.Navigation("userTeacherClass");
                });
#pragma warning restore 612, 618
        }
    }
}
