﻿using GraphQL.Data;
using GraphQL.Data.Context;
using GraphQL.Data.Models;
using GraphQL.DataLoader;
using GraphQL.Extensions;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQL.Types
{
    public class SessionType : ObjectType<Session>
    {
        protected override void Configure(IObjectTypeDescriptor<Session> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<SessionByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.SessionSpeakers)
                .ResolveWith<SessionResolvers>(t => t.GetSpeakersAsync(default!, default!, default!, default!))
                .UseDbContext<ApplicationDbContext>()
                .Name("speakers");

            descriptor
                .Field(t => t.SessionAttendees)
                .ResolveWith<SessionResolvers>(t => t.GetAttendeesAsync(default!, default!, default!, default!))
                .UseDbContext<ApplicationDbContext>()
                .Name("attendees");
        }

        private class SessionResolvers
        {
            //Resolver do relacionamento Session <--> Speakers
            public async Task<IEnumerable<Speaker>> GetSpeakersAsync(Session session,
                [ScopedService] ApplicationDbContext dbContext, 
                SpeakerByIdDataLoader speakerById,
                CancellationToken cancellationToken)
            {
                int[] speakerIds = await dbContext.Sessions
                    .Where(s => s.Id == session.Id)
                    .Include(s => s.SessionSpeakers)
                    .SelectMany(s => s.SessionSpeakers.Select(t => t.SpeakerId))
                    .ToArrayAsync();

                return await speakerById.LoadAsync(speakerIds, cancellationToken);
            }

            //Resolver do relacionamento Session <--> Attendees
            public async Task<IEnumerable<Attendee>> GetAttendeesAsync(Session session, 
                [ScopedService] ApplicationDbContext dbContext, 
                AttendeeByIdDataLoader attendeeById,
                CancellationToken cancellationToken)
            {
                int[] attendeeIds = await dbContext.Sessions
                    .Where(s => s.Id == session.Id)
                    .Include(session => session.SessionAttendees)
                    .SelectMany(session => session.SessionAttendees.Select(t => t.AttendeeId))
                    .ToArrayAsync();

                return await attendeeById.LoadAsync(attendeeIds, cancellationToken);
            }

            //Resolver do relacionamento Session <--> Track
            public async Task<Track?> GetTrackAsync(Session session, 
                TrackByIdDataLoader trackById, 
                CancellationToken cancellationToken)
            {
                if (session.TrackId is null) return null;

                return await trackById.LoadAsync(session.TrackId.Value, cancellationToken);
            }
        }
    }
}
