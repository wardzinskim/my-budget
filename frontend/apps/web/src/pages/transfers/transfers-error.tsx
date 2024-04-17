import { Box, Container, Typography } from '@mui/material';

export const TransferErrorPage: React.FC = () => {
  return (
    <>
      <Container>
        <Box
          sx={{
            py: 12,
            maxWidth: 480,
            mx: 'auto',
            display: 'flex',
            minHeight: '100vh',
            textAlign: 'center',
            alignItems: 'center',
            flexDirection: 'column',
            justifyContent: 'center',
          }}
        >
          <Typography variant="h3" sx={{ mb: 3 }}>
            Sorry, budget is not selected!
          </Typography>

          <Typography sx={{ color: 'text.secondary' }}>
            Please select the budget
          </Typography>
        </Box>
      </Container>
    </>
  );
};
