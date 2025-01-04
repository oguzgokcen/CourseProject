import React from 'react';
import Rating from '@mui/material/Rating';
import Radio from '@mui/material/Radio';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormControl from '@mui/material/FormControl';
import RadioGroup from '@mui/material/RadioGroup';

const RatingFilter = ({ selectedRating, onRatingChange }) => {
    const ratings = [
        { value: 4.5, label: '4.5 or higher' },
        { value: 4.0, label: '4.0 or higher' },
        { value: 3.5, label: '3.5 or higher' },
        { value: 3.0, label: '3.0 or higher' }
    ];

    return (
        <div style={styles.container}>
            <h5 style={styles.subtitle}>Ratings</h5>
            <FormControl style={{ width: '100%' }}>
                <RadioGroup
                    value={selectedRating || ''}
                    onChange={(e) => onRatingChange(Number(e.target.value))}
                >
                    {ratings.map((rating) => (
                        <FormControlLabel
                            key={rating.value}
                            value={rating.value}
                            control={<Radio size="small" />}
                            label={
                                <div style={styles.ratingLabel}>
                                    <Rating 
                                        value={rating.value}
                                        precision={0.5}
                                        readOnly
                                        size="small"
                                        style={{ fontSize: '16px' }}
                                    />
                                    <span style={styles.ratingText}>{rating.label}</span>
                                </div>
                            }
                            style={styles.formControl}
                        />
                    ))}
                </RadioGroup>
            </FormControl>
        </div>
    );
};

const styles = {
    container: {
        padding: '20px 0',
        width: '240px'
    },
    subtitle: {
        fontSize: '16px',
        fontWeight: 600,
        margin: '0 0 15px 0'
    },
    resetButton: {
        background: 'none',
        border: 'none',
        color: '#5624d0',
        cursor: 'pointer',
        fontSize: '13px',
        padding: '4px 8px',
        borderRadius: '4px',
        ':hover': {
            backgroundColor: '#f7f9fa'
        }
    },
    ratingLabel: {
        display: 'flex',
        alignItems: 'center',
        gap: '8px',
        width: '100%'
    },
    ratingText: {
        fontSize: '13px',
        color: '#6a6f73',
        whiteSpace: 'nowrap'
    },
    formControl: {
        marginLeft: '-8px',
        marginRight: 0
    }
};

export default RatingFilter; 