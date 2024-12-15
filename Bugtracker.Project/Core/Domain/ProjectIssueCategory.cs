﻿using System;

namespace BugTracker.Domain
{
    /// <summary>
    /// Классификатор назначенных на проекты категории задач и ответственный пользователь
    /// </summary>
    /// <remarks>
    /// Категории задач, это распределение по модулям/сервисам/функционалу. Например: Модуль Wiki, Модуль Управление пользователями
    /// Каждой категории назначается ответственный человек, и при выборе категории для задачи, автоматически наначается пользователь
    /// </remarks>
    public class ProjectIssueCategory
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор проекта для которого создана категория
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Имя категории
        /// </summary>
        /// <remarks>
        /// Например: Модуль Wiki, Модуль Управление пользователями
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор пользователя, на которого назначена категория. Может быть null.
        /// </summary>
        /// <remarks>
        /// Каждой категории назначается ответственный человек, и при выборе категории для задачи, автоматически наначается пользователь
        /// </remarks>
        public Guid UserId { get; set; }
    }
}
