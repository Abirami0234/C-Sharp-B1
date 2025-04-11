using System.Collections.Generic;
using System.Linq;


namespace CarConnect
{
    public class ReservationService : IReservationService
    {
        private List<Reservation> reservations = new List<Reservation>();

        public Reservation GetReservationById(int reservationId) =>
            reservations.FirstOrDefault(r => r.ReservationID == reservationId);

        public List<Reservation> GetReservationsByCustomerId(int customerId) =>
            reservations.Where(r => r.CustomerID == customerId).ToList();

        public void CreateReservation(Reservation reservation) =>
            reservations.Add(reservation);

        public void UpdateReservation(Reservation reservation)
        {
            var existing = GetReservationById(reservation.ReservationID);
            if (existing != null)
            {
                reservations.Remove(existing);
                reservations.Add(reservation);
            }
        }

        public void CancelReservation(int reservationId) =>
            reservations.RemoveAll(r => r.ReservationID == reservationId);
    }
}
