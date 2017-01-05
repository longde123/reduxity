﻿using Redux;
using System;
using ExitGames.Client.Photon;

namespace Reduxity.Example.Zenject.RoomConnector {
    public class Action {
        /// <summary>
        /// Properties for each type of http request
        /// </summary>
        public abstract class IRoomAction {
            public string feedbackText { get; set; }
            public string roomName { get; set; }
        }

        public class JoinStart : IRoomAction, IAction {}
        public class JoinSuccess : IRoomAction, IAction {}
        public class JoinFailure : IRoomAction, IAction {}
        public class CreateStart : IRoomAction, IAction {}
        public class CreateSuccess : IRoomAction, IAction {}
        public class CreateFailure : IRoomAction, IAction {}
        public class LeaveStart : IRoomAction, IAction {}
        public class LeaveSuccess : IRoomAction, IAction {}
        public class LeaveFailure : IRoomAction, IAction {}
        public class UpdateRoomProperties : IAction {
            public Hashtable roomProperties { get; set; }
        }
    }

    public class Reducer : IReducer {
        readonly Settings settings_;

        public Reducer(Settings settings) {
            settings_ = settings;
        }

        public RoomState Reduce(RoomState state, IAction action) {
            if (action is Action.JoinStart) {
                return StartJoin(state, (Action.JoinStart)action);
            }

            if (action is Action.JoinSuccess) {
                return JoinSuccess(state, (Action.JoinSuccess)action);
            }

            if (action is Action.JoinFailure) {
                return JoinFailure(state, (Action.JoinFailure)action);
            }

            if (action is Action.CreateStart) {
                return StartCreate(state, (Action.CreateStart)action);
            }

            if (action is Action.CreateSuccess) {
                return CreateSuccess(state, (Action.CreateSuccess)action);
            }

            if (action is Action.CreateFailure) {
                return CreateFailure(state, (Action.CreateFailure)action);
            }

            if (action is Action.LeaveStart) {
                return Leave(state, (Action.LeaveStart)action);
            }

            if (action is Action.LeaveSuccess) {
                return LeaveSuccess(state, (Action.LeaveSuccess)action);
            }

            if (action is Action.UpdateRoomProperties) {
                return UpdateRoomProperties(state, (Action.UpdateRoomProperties)action);
            }

            return state;
        }

        private RoomState StartJoin(RoomState state, Action.JoinStart action) {
            state.isJoining = true;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isCreating = false;
            state.isLeaving = false;
            state.feedbackText = "Joining room...";
            state.roomName = action.roomName;
            return state;
        }

        private RoomState JoinSuccess(RoomState state, Action.JoinSuccess action) {
            state.isJoining = false;
            state.isJoined = true;
            state.isJoinFailed = false;
            state.isCreating = false;
            state.isLeaving = false;
            state.feedbackText = "Joined room.";
            state.roomName = action.roomName;
            return state;
        }

        private RoomState JoinFailure(RoomState state, Action.JoinFailure action) {
            state.isJoining = false;
            state.isJoined = false;
            state.isJoinFailed = true;
            state.isCreating = false;
            state.isLeaving = false;
            state.feedbackText = "Joining room failed.";
            state.roomName = null;
            return state;
        }

        private RoomState StartCreate(RoomState state, Action.CreateStart action) {
            state.isJoining = true;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isCreating = true;
            state.isCreated = false;
            state.isCreateFailed = false;
            state.isLeaving = false;
            state.feedbackText = "Creating room...";
            state.roomName = action.roomName;
            return state;
        }

        private RoomState CreateSuccess(RoomState state, Action.CreateSuccess action) {
            state.isJoining = false;
            state.isJoined = true;
            state.isJoinFailed = false;
            state.isCreating = false;
            state.isCreated = true;
            state.isCreateFailed = false;
            state.isLeaving = false;
            state.feedbackText = "Created room.";
            state.roomName = action.roomName;
            return state;
        }

        private RoomState CreateFailure(RoomState state, Action.CreateFailure action) {
            state.isJoining = false;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isCreating = false;
            state.isCreated = false;
            state.isCreateFailed = true;
            state.isLeaving = false;
            state.feedbackText = "Creating room failed.";
            state.roomName = action.roomName;
            return state;
        }

        private RoomState Leave(RoomState state, Action.LeaveStart action) {
            state.isJoining = false;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isLeaving = true;
            state.feedbackText = "Leaving room...";
            state.roomName = action.roomName;
            return state;
        }

        private RoomState LeaveSuccess(RoomState state, Action.LeaveSuccess action) {
            state.isJoining = false;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isLeaving = false;
            state.feedbackText = "Left room";
            state.roomName = null;
            return state;
        }

        private RoomState UpdateRoomProperties(RoomState state, Action.UpdateRoomProperties action) {
            state.roomProperties = action.roomProperties;
            return state;
        }
    }

    [Serializable]
    /// <summary>
    /// Public settings for api loading
    /// </summary>
    public class Settings {

        // The maximum number of players per room
        public byte maxPlayersPerRoom = 4;

		// #Critical: The first we try to do is to join a potential existing room.
		// If there is, good, else, we'll be called back with OnPhotonRandomJoinFailed()
		public bool shouldJoinRandomRoom = true;
    }
}